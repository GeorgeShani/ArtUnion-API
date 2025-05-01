using AutoMapper;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<LoginRequest, User>();
        CreateMap<User, AuthDTO>();
    }
}