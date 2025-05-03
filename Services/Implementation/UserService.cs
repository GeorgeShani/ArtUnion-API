using ArtUnion_API.DTOs;
using FluentValidation;
using ArtUnion_API.Models;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Services.Interfaces;
using AutoMapper;

namespace ArtUnion_API.Services.Implementation;

public class UserService : IUserService
{
    private readonly IValidator<UpdateUserRequest> _updateUserRequestValidator;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IRepository<User> userRepository, 
        IAmazonS3Service amazonS3Service, 
        IValidator<UpdateUserRequest> updateUserRequestValidator, 
        IMapper mapper
    ) {
        _userRepository = userRepository;
        _amazonS3Service = amazonS3Service;
        _updateUserRequestValidator = updateUserRequestValidator;
        _mapper = mapper;
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        var userDtos = _mapper.Map<List<UserDTO>>(users);
        return userDtos;       
    }

    public async Task<UserDTO?> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        var userDto = _mapper.Map<UserDTO>(user);
        return userDto;       
    }

    public async Task<UserDTO> UpdateUser(int id, UpdateUserRequest request)
    {
        var validationResult = await _updateUserRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found.");
        
        user = _mapper.Map(request, user);
        
        if (!string.IsNullOrWhiteSpace(request.Password))
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        if (request.ProfilePicture != null)
        {
            var fullFileName = Path.GetFileName(request.ProfilePicture.FileName);
            var imageUrl = await _amazonS3Service.UploadImageToS3(
                request.ProfilePicture, $"avatars/{Guid.NewGuid()}_{fullFileName}"
            );
            
            user.ProfilePictureUrl = imageUrl;
        }
        
        var updatedUser = await _userRepository.UpdateAsync(user);
        var userDto = _mapper.Map<UserDTO>(updatedUser);
        return userDto;       
    }

    public async Task<UserDTO> DeleteUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found.");
        
        return _mapper.Map<UserDTO>(await _userRepository.DeleteAsync(user));       
    }
}