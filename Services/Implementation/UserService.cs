using FluentValidation;
using ArtUnion_API.Models;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class UserService : IUserService
{
    private readonly IValidator<UpdateUserRequest> _updateUserRequestValidator;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IRepository<User> _userRepository;

    public UserService(
        IRepository<User> userRepository, 
        IAmazonS3Service amazonS3Service, 
        IValidator<UpdateUserRequest> updateUserRequestValidator
    ) {
        _userRepository = userRepository;
        _amazonS3Service = amazonS3Service;
        _updateUserRequestValidator = updateUserRequestValidator;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> UpdateUser(int id, UpdateUserRequest request)
    {
        var validationResult = await _updateUserRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found.");
        
        SetIfNotEmpty(request.FirstName, val => user.FirstName = val);
        SetIfNotEmpty(request.LastName, val => user.LastName = val);
        SetIfNotEmpty(request.Username, val => user.Username = val);
        SetIfNotEmpty(request.Biography, val => user.Biography = val);
        
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
        return updatedUser;       
    }

    public async Task<User> DeleteUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found.");
        
        return await _userRepository.DeleteAsync(user);;       
    }
    
    private static void SetIfNotEmpty(string? value, Action<string> setter)
    {
        if (!string.IsNullOrWhiteSpace(value)) setter(value);
    }
}