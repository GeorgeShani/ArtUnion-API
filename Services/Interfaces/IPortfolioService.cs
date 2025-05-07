using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Services.Interfaces;

public interface IPortfolioService
{
    Task<List<PortfolioDTO>> GetAllPortfolios();
    Task<PortfolioDTO?> GetPortfolioById(int id);
    Task<PortfolioDTO> CreatePortfolio(CreatePortfolioRequest request);
    Task<PortfolioDTO> UpdatePortfolio(int id, UpdatePortfolioRequest request);
    Task<PortfolioDTO> DeletePortfolio(int id);
}