using AutoMapper;
using FluentValidation;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Configs;
using ArtUnion_API.Helpers;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IValidator<RegisterRequest> _registerRequestValidator;
    private readonly IValidator<LoginRequest> _loginRequestValidator;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IEmailService _emailService;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public AuthService(
        IRepository<User> userRepository, 
        IValidator<RegisterRequest> registerRequestValidator, 
        IValidator<LoginRequest> loginRequestValidator, 
        IAmazonS3Service amazonS3Service, 
        IEmailService emailService, 
        IJwtService jwtService, 
        IMapper mapper
    ) {
        _userRepository = userRepository;
        _registerRequestValidator = registerRequestValidator;
        _loginRequestValidator = loginRequestValidator;
        _amazonS3Service = amazonS3Service;
        _emailService = emailService;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<AuthDTO> SignUp(RegisterRequest request)
    {
        var validationResult = await _registerRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        string? avatarUrl = null;
        if (request.ProfilePicture != null)
        {
            var fullFileName = Path.GetFileName(request.ProfilePicture.FileName);
            avatarUrl = await _amazonS3Service.UploadImageToS3(
                request.ProfilePicture,
                $"avatars/{Guid.NewGuid()}_{fullFileName}"
            );
        }

        var user = _mapper.Map<User>(request);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.ProfilePictureUrl = avatarUrl;
        user.VerificationCode = VerificationCodeGenerator.Generate6DigitCode();
        user.CodeDeadline = DateTime.UtcNow.AddDays(1);
        
        await _userRepository.CreateAsync(user);
        await _emailService.SendEmailAsync(
            user.Email, 
            "Welcome to ArtUnion 🎨 – Let’s Verify Your Email",
            EmailTemplates.VerifyEmail(user)
        );
        
        return MapToAuthDTO(user);       
    }

    public async Task<AuthDTO> LogIn(LoginRequest request)
    {
        var validationResult = await _loginRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
            throw new UnauthorizedAccessException("Invalid email or password.");
        
        return MapToAuthDTO(existingUser);       
    }
    
    public async Task<AuthDTO> VerifyEmail(string verificationCode)
    {
        if (string.IsNullOrWhiteSpace(verificationCode))
            throw new ArgumentException("Verification code is required.", nameof(verificationCode));

        var user = await _userRepository.FirstOrDefaultAsync(u => u.VerificationCode == verificationCode);
        if (user is null)
            throw new InvalidOperationException("Invalid or expired verification code.");

        if (user.IsVerified)
            throw new InvalidOperationException("User is already verified.");

        if (user.CodeDeadline < DateTime.UtcNow)
            throw new InvalidOperationException("Verification code has expired.");

        user.IsVerified = true;
        user.VerifiedAt = DateTime.UtcNow;
        user.VerificationCode = null;
        user.CodeDeadline = null;

        await _userRepository.UpdateAsync(user);
        return MapToAuthDTO(user);
    }


    private AuthDTO MapToAuthDTO(User user)
    {
        var authDTO = _mapper.Map<AuthDTO>(user);
        authDTO.Token = _jwtService.GenerateToken(user);
        return authDTO; 
    }
}