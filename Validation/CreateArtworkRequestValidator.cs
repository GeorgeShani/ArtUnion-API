using FluentValidation;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Validation;

public class CreateArtworkRequestValidator : AbstractValidator<CreateArtworkRequest>
{
    public CreateArtworkRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(BeAValidUrl).WithMessage("Image URL must be a valid URL.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must be 1000 characters or fewer.")
            .When(x => x.Description != null);

        RuleFor(x => x.ArtistId)
            .GreaterThan(0).WithMessage("ArtistId must be a positive number.");

        RuleFor(x => x.PortfolioId)
            .GreaterThan(0).WithMessage("PortfolioId must be a positive number.")
            .When(x => x.PortfolioId.HasValue);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a positive number.");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
