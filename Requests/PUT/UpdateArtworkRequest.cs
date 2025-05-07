namespace ArtUnion_API.Requests.PUT;

public class UpdateArtworkRequest
{
    public string? Title { get; set; }
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
    public int? PortfolioId { get; set; }
    public int? CategoryId { get; set; }
}