using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthDTO> SignUp(RegisterRequest request);
    Task<AuthDTO> LogIn(LoginRequest request);
    Task<AuthDTO> VerifyEmail(string verificationCode);
}