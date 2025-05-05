using ArtUnion_API.Requests.PUT;
using FluentValidation;

namespace ArtUnion_API.Validation;

public class UpdateArtworkRequestValidator : AbstractValidator<UpdateArtworkRequest>
{
    public UpdateArtworkRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty when provided.")
            .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.")
            .When(x => x.Title != null);

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL cannot be empty when provided.")
            .Must(BeAValidUrl).WithMessage("Image URL must be a valid URL.")
            .When(x => x.ImageUrl != null);

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must be 1000 characters or fewer.")
            .When(x => x.Description != null);

        RuleFor(x => x.PortfolioId)
            .GreaterThan(0).WithMessage("PortfolioId must be a positive number.")
            .When(x => x.PortfolioId.HasValue);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a positive number.")
            .When(x => x.CategoryId.HasValue);
    }

    private static bool BeAValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
