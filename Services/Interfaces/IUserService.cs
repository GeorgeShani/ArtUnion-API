using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDTO>> GetAllUsers();
    Task<UserDTO?> GetUserById(int id);
    Task<UserDTO> UpdateUser(int id, UpdateUserRequest request);
    Task<UserDTO> DeleteUser(int id);
}