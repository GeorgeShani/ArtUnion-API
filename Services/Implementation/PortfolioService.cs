using AutoMapper;
using FluentValidation;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class PortfolioService : IPortfolioService
{
    private readonly IRepository<Portfolio> _portfolioRepository;
    private readonly IValidator<CreatePortfolioRequest> _createPortfolioRequestValidator;
    private readonly IValidator<UpdatePortfolioRequest> _updatePortfolioRequestValidator;
    private readonly IMapper _mapper;

    public PortfolioService(
        IRepository<Portfolio> portfolioRepository, 
        IValidator<CreatePortfolioRequest> createPortfolioRequestValidator,
        IValidator<UpdatePortfolioRequest> updatePortfolioRequestValidator, 
        IMapper mapper 
    ) {
        _portfolioRepository = portfolioRepository;
        _updatePortfolioRequestValidator = updatePortfolioRequestValidator;
        _createPortfolioRequestValidator = createPortfolioRequestValidator;
        _mapper = mapper;
    }

    public async Task<List<PortfolioDTO>> GetAllPortfolios()
    {
        var portfolios = await _portfolioRepository.GetAllAsync();
        var portfolioDtos = _mapper.Map<List<PortfolioDTO>>(portfolios);
        return portfolioDtos;       
    }

    public async Task<PortfolioDTO?> GetPortfolioById(int id)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(id);
        var portfolioDto = _mapper.Map<PortfolioDTO>(portfolio);
        return portfolioDto;      
    }

    public async Task<PortfolioDTO> CreatePortfolio(CreatePortfolioRequest request)
    {
        var validationResult = await _createPortfolioRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var portfolio = _mapper.Map<Portfolio>(request);
        var createdPortfolio = await _portfolioRepository.CreateAsync(portfolio);
        return _mapper.Map<PortfolioDTO>(createdPortfolio);      
    }

    public async Task<PortfolioDTO> UpdatePortfolio(int id, UpdatePortfolioRequest request)
    {
        var validationResult = await _updatePortfolioRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var portfolio = await _portfolioRepository.GetByIdAsync(id);
        if (portfolio == null)
            throw new Exception("Portfolio not found.");
        
        portfolio = _mapper.Map(request, portfolio);
        var updatedPortfolio = await _portfolioRepository.UpdateAsync(portfolio);
        return _mapper.Map<PortfolioDTO>(updatedPortfolio);      
    }

    public async Task<PortfolioDTO> DeletePortfolio(int id)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(id);
        if (portfolio == null)
            throw new Exception("Portfolio not found.");
        
        return _mapper.Map<PortfolioDTO>(await _portfolioRepository.DeleteAsync(portfolio));      
    }
}