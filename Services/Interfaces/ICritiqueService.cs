using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Services.Interfaces;

public interface ICritiqueService
{
    Task<List<CritiqueDTO>> GetCritiquesByArtwork(int artworkId);
    Task<CritiqueDTO> CreateCritique(CreateCritiqueRequest request);
    Task<CritiqueDTO> UpdateCritique(int id, UpdateCritiqueRequest request);
    Task<CritiqueDTO> DeleteCritique(int id);
}