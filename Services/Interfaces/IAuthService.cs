using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Services.Interfaces;

public interface IAuthService
{
    AuthDTO SignUp(RegisterRequest request);
    AuthDTO LogIn(LoginRequest request);
}