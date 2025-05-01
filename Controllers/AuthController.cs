using ArtUnion_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
}