using AutoMapper;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<LoginRequest, User>();
        CreateMap<User, AuthDTO>();
        CreateMap<User, UserDTO>();
        
        CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore()) // handle password manually
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.Ignore()) // handle file upload manually
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) =>
                srcMember != null && 
                !(srcMember is string str && string.IsNullOrEmpty(str))
            ));
        
        CreateMap<CreatePortfolioRequest, Portfolio>();
        CreateMap<UpdatePortfolioRequest, Portfolio>();
        CreateMap<Portfolio, PortfolioDTO>();
        
        CreateMap<CreateArtworkRequest, Artwork>();
        CreateMap<UpdateArtworkRequest, Artwork>();
        CreateMap<Artwork, ArtworkDTO>();
        
        CreateMap<Category, CategoryDTO>();
    }
}