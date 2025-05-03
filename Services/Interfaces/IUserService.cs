using ArtUnion_API.Models;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User> UpdateUser(int id, UpdateUserRequest request);
    Task<User> DeleteUser(int id);
}