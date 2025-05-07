namespace ArtUnion_API.DTOs;

public class ArtworkFilterDTO
{
    public int? ArtistId { get; set; }
    public int? CategoryId { get; set; }
    public int? PortfolioId { get; set; }
    public string? SearchTerm { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? SortBy { get; set; }
    public bool Descending { get; set; } = true;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}