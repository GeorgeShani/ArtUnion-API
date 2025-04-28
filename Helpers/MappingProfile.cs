using AutoMapper;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;

namespace ArtUnion_API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, AuthDTO>();
    }
}