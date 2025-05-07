using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Services.Interfaces;

public interface IArtworkService
{
    Task<List<ArtworkDTO>> GetArtworks(ArtworkFilterDTO filter);
    Task<ArtworkDTO?> GetArtworkById(int id);
    Task<List<ArtworkDTO>> GetArtworksByCategory(int categoryId);
    Task<ArtworkDTO> CreateArtwork(CreateArtworkRequest request);
    Task<ArtworkDTO> UpdateArtwork(int id, UpdateArtworkRequest request);
    Task<ArtworkDTO> DeleteArtwork(int id);
}