using AutoMapper;
using FluentValidation;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Configs;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;
using Microsoft.EntityFrameworkCore;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class ArtworkService : IArtworkService
{
    private readonly IRepository<Artwork> _artworkRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IValidator<CreateArtworkRequest> _createArtworkRequestValidator;
    private readonly IValidator<UpdateArtworkRequest> _updateArtworkRequestValidator;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public ArtworkService(
        IRepository<Artwork> artworkRepository, 
        IRepository<User> userRepository,
        IValidator<CreateArtworkRequest> createArtworkRequestValidator, 
        IValidator<UpdateArtworkRequest> updateArtworkRequestValidator, 
        IAmazonS3Service amazonS3Service,
        IEmailService emailService,
        IMapper mapper
    ) {
        _artworkRepository = artworkRepository;
        _userRepository = userRepository;
        _createArtworkRequestValidator = createArtworkRequestValidator;
        _updateArtworkRequestValidator = updateArtworkRequestValidator;
        _amazonS3Service = amazonS3Service;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<List<ArtworkDTO>> GetArtworks(ArtworkFilterDTO filter)
    {
        var query = _artworkRepository.Query();
        if (filter.ArtistId.HasValue)
            query = query.Where(a => a.ArtistId == filter.ArtistId);

        if (filter.CategoryId.HasValue)
            query = query.Where(a => a.CategoryId == filter.CategoryId);

        if (filter.PortfolioId.HasValue)
            query = query.Where(a => a.PortfolioId == filter.PortfolioId);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.ToLower();
            query = query.Where(a => 
                a.Title.ToLower().Contains(term) || 
                (a.Description != null && a.Description.ToLower().Contains(term))
            );
        }

        if (filter.DateFrom.HasValue)
            query = query.Where(a => a.CreatedAt >= filter.DateFrom.Value);

        if (filter.DateTo.HasValue)
            query = query.Where(a => a.CreatedAt <= filter.DateTo.Value);
        
        if (!string.IsNullOrWhiteSpace(filter.SortBy)) // Sorting
        {
            query = filter.SortBy.ToLower() switch
            {
                "likes" => filter.Descending
                    ? query.OrderByDescending(a => a.Likes!.Count)
                    : query.OrderBy(a => a.Likes!.Count),

                "critiques" => filter.Descending
                    ? query.OrderByDescending(a => a.Critiques!.Count)
                    : query.OrderBy(a => a.Critiques!.Count),

                _ => filter.Descending
                    ? query.OrderByDescending(a => a.CreatedAt)
                    : query.OrderBy(a => a.CreatedAt),
            };
        }
        else
        {
            query = query.OrderByDescending(a => a.CreatedAt);
        }
        
        query = query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);  // Apply pagination

        var artworks = await query.ToListAsync();
        return _mapper.Map<List<ArtworkDTO>>(artworks);
    }

    public async Task<ArtworkDTO?> GetArtworkById(int id)
    {
        var artwork = await _artworkRepository.GetByIdAsync(id);
        var artworkDto = _mapper.Map<ArtworkDTO>(artwork);
        return artworkDto;
    }

    public async Task<List<ArtworkDTO>> GetArtworksByCategory(int categoryId)
    {
        var artworksByCategory = await _artworkRepository.Query()
            .Where(a => a.CategoryId == categoryId)
            .ToListAsync();
        
        return _mapper.Map<List<ArtworkDTO>>(artworksByCategory);
    }

    public async Task<ArtworkDTO> CreateArtwork(CreateArtworkRequest request)
    {
        var validationResult = await _createArtworkRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var artwork = _mapper.Map<Artwork>(request);
        
        var fullFileName = Path.GetFileName(request.Image.FileName);
        artwork.ImageUrl = await _amazonS3Service.UploadImageToS3(
            request.Image, $"artworks/{Guid.NewGuid()}_{fullFileName}"
        );
        
        var createdArtwork = await _artworkRepository.CreateAsync(artwork);
        
        var artist = await _userRepository.GetByIdAsync(request.ArtistId);
        if (artist == null)
            throw new Exception("Artist not found.");
        
        var subscribers = await _userRepository
            .Query()
            .Include(u => u.Following)
            .Where(u => u.Following!.Any(f => f.ArtistId == request.ArtistId))
            .ToListAsync();
        
        foreach (var subscriber in subscribers)
        {
            await _emailService.SendEmailAsync(
                subscriber.Email,
                $"🆕 New Artwork by {artist.FirstName} {artist.LastName}: \"{createdArtwork.Title}\"",
                EmailTemplates.NewArtworkNotification(subscriber, artist, createdArtwork)
            );
        } 
        
        return _mapper.Map<ArtworkDTO>(createdArtwork);
    }

    public async Task<ArtworkDTO> UpdateArtwork(int id, UpdateArtworkRequest request)
    {
        var validationResult = await _updateArtworkRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid) 
            throw new ValidationException(validationResult.Errors);
        
        var artwork = await _artworkRepository.GetByIdAsync(id);
        if (artwork == null)
            throw new Exception("Artwork not found.");
        
        artwork = _mapper.Map(request, artwork);

        if (request.Image != null)
            artwork.ImageUrl = await _amazonS3Service.UploadImageToS3(
                request.Image, $"artworks/{Guid.NewGuid()}_{Path.GetFileName(request.Image.FileName)}"
            );
        
        var updatedArtwork = await _artworkRepository.UpdateAsync(artwork);
        return _mapper.Map<ArtworkDTO>(updatedArtwork);
    }

    public async Task<ArtworkDTO> DeleteArtwork(int id)
    {
        var artwork = await _artworkRepository.GetByIdAsync(id);
        if (artwork == null)
            throw new Exception("Artwork not found.");

        return _mapper.Map<ArtworkDTO>(await _artworkRepository.DeleteAsync(artwork));
    }
}