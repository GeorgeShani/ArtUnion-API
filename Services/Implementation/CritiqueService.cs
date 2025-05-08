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

public class CritiqueService : ICritiqueService
{
    private readonly IRepository<Critique> _critiqueRepository;
    private readonly IRepository<Artwork> _artworkRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IValidator<CreateCritiqueRequest> _createCritiqueRequestValidator;
    private readonly IValidator<UpdateCritiqueRequest> _updateCritiqueRequestValidator;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public CritiqueService(
        IRepository<Critique> critiqueRepository, 
        IRepository<Artwork> artworkRepository, 
        IRepository<User> userRepository, 
        IValidator<CreateCritiqueRequest> createCritiqueRequestValidator, 
        IValidator<UpdateCritiqueRequest> updateCritiqueRequestValidator, 
        IEmailService emailService,
        IMapper mapper
    ) {
        _critiqueRepository = critiqueRepository;
        _artworkRepository = artworkRepository;
        _userRepository = userRepository;
        _createCritiqueRequestValidator = createCritiqueRequestValidator;
        _updateCritiqueRequestValidator = updateCritiqueRequestValidator;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<List<CritiqueDTO>> GetCritiquesByArtwork(int artworkId)
    {
        var artwork = await _artworkRepository.GetByIdAsync(artworkId);
        if (artwork == null)
            throw new Exception("Artwork not found.");
        
        var critiquesByArtwork = await _critiqueRepository
            .Query(c => c.ArtworkId == artworkId)
            .ToListAsync();
        
        return _mapper.Map<List<CritiqueDTO>>(critiquesByArtwork);
    }

    public async Task<CritiqueDTO> CreateCritique(CreateCritiqueRequest request)
    {
        var validationResult = await _createCritiqueRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var artwork = await _artworkRepository.GetByIdAsync(request.ArtworkId);
        if (artwork == null)
            throw new Exception("Artwork not found.");
        
        var artist = await _userRepository.GetByIdAsync(artwork.ArtistId);
        if (artist == null)
            throw new Exception("Artist not found.");
        
        var critic = await _userRepository.GetByIdAsync(request.CriticId);
        if (critic == null)
            throw new Exception("Critic not found.");
        
        var critique = _mapper.Map<Critique>(request);
        var createdCritique = await _critiqueRepository.CreateAsync(critique);

        var emailContent = EmailTemplates.NewCommentNotification(artist, critic, artwork, createdCritique);
        var criticFullName = $"{critic.FirstName} {critic.LastName}";
        
        await _emailService.SendEmailAsync(
            artist.Email,
            $"🎨 New Critique on \"{artwork.Title}\" by {criticFullName}",
            emailContent
        );

        
        return _mapper.Map<CritiqueDTO>(createdCritique);
    }

    public async Task<CritiqueDTO> UpdateCritique(int id, UpdateCritiqueRequest request)
    {
        var validationResult = await _updateCritiqueRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var critique = await _critiqueRepository.GetByIdAsync(id);
        if (critique == null)
            throw new Exception("Critique not found.");
        
        critique = _mapper.Map(request, critique);
        var updatedCritique = await _critiqueRepository.UpdateAsync(critique);
        return _mapper.Map<CritiqueDTO>(updatedCritique);
    }

    public async Task<CritiqueDTO> DeleteCritique(int id)
    {
        var critique = await _critiqueRepository.GetByIdAsync(id);
        if (critique == null)
            throw new Exception("Critique not found.");
        
        return _mapper.Map<CritiqueDTO>(await _critiqueRepository.DeleteAsync(critique));
    }
}