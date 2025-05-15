using ArtUnion_API.Enums;
using ArtUnion_API.Models;
using ArtUnion_API.Configs;
using ArtUnion_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtUnion_API.Services.Implementation;

public class WeeklyDigestService : IWeeklyDigestService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Artwork> _artworkRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<WeeklyDigestService> _logger;
    
    public WeeklyDigestService(
        IRepository<User> userRepository, 
        IRepository<Artwork> artworkRepository, 
        IEmailService emailService, 
        ILogger<WeeklyDigestService> logger
    ) {
        _userRepository = userRepository;
        _artworkRepository = artworkRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task SendWeeklyDigestAsync()
    {
        try
        {
            // Get the date range for this week
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-7);
            var weekRange = $"{startDate:MMM d} - {endDate:MMM d, yyyy}";

            // Get popular artworks based on likes and comments from the last week
            var popularArtworks = await GetPopularArtworksAsync();
            if (popularArtworks.Count == 0)
            {
                _logger.LogInformation("No popular artworks this week. Skipping weekly digest.");
                return;
            }

            // Get all users
            var users = await _userRepository
                .Query(u => u.IsVerified && u.Role != Role.Admin)
                .ToListAsync();

            _logger.LogInformation("Sending weekly digest to {UsersCount} users", users.Count);
            
            foreach (var user in users)
            {
                // Generate personalized email content
                var emailBody = EmailTemplates.WeeklyDigest($"{user.FirstName} {user.LastName}", weekRange, popularArtworks);
            
                // Send email
                await _emailService.SendEmailAsync(
                    user.Email, 
                    "🖼️ Your Weekly Art Highlights – Top 10 Picks You Shouldn't Miss!",
                    emailBody
                );
            }
            
            _logger.LogInformation("Weekly digest sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending weekly digest");
            throw;
        }
    }

    private async Task<List<Artwork>> GetPopularArtworksAsync()
    {
        // Define the time range for considering "recent" artworks (last 7 days)
        var lastWeek = DateTime.UtcNow.AddDays(-7);
        
        var artworks = await _artworkRepository.Query()
            // Only consider artworks created in the last 7 days
            .Where(a => a.CreatedAt >= lastWeek)
            
            // Eager load-related entities to avoid lazy loading performance hits
            .Include(a => a.Artist)        // Include the artist info (User)
            .Include(a => a.Likes)         // Include likes (ArtworkLike collection)
            .Include(a => a.Critiques)     // Include critiques (Critique collection)
            .Include(a => a.Category)      // Include category info (Category)
            .Select(a => new  // Project each artwork into an anonymous object with calculated values
            {
                Artwork = a,
                LikesCount = a.Likes!.Count > 0 ? a.Likes!.Count : 0,
                CritiquesCount = a.Critiques!.Count > 0 ? a.Critiques!.Count: 0,
                
                // If there are critiques, calculate the average rating (assuming 'Rating' exists)
                AvgCritiqueRating = a.Critiques!.Count > 0 ? a.Critiques.Average(c => c.Rating) : 0,
                
                // Calculate how many days have passed since creation
                DaysSinceCreated = EF.Functions.DateDiffDay(a.CreatedAt, DateTime.Now)
            })
            .ToListAsync();

        return artworks
            // Apply a scoring formula to determine popularity
            .Select(a => new
            {
                a.Artwork,
                Score =
                    a.LikesCount * 2.0 +            // Likes are weighted heavily
                    a.CritiquesCount * 1.5 +        // Critique count has moderate weight
                    a.AvgCritiqueRating * 2.5 -     // High-average ratings increase the score
                    a.DaysSinceCreated * 0.3        // Older artworks lose some weight
            })

            // Order artworks by descending score (most popular first)
            .OrderByDescending(x => x.Score)

            // Take the top 10 most popular artworks
            .Take(10)

            // Select only the Artwork objects from the projection
            .Select(x => x.Artwork)

            // Convert the result to a List asynchronously
            .ToList();
    }
}