using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class AuthService : IAuthService
{
    private Repository<User> _userRepository;

    public AuthService(Repository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public AuthDTO SignUp(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public AuthDTO LogIn(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}