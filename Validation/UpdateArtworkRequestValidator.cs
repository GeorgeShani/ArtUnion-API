using FluentValidation;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Validation;

public class UpdateArtworkRequestValidator : AbstractValidator<UpdateArtworkRequest>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];

    public UpdateArtworkRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty when provided.")
            .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.")
            .When(x => x.Title != null);

        RuleFor(x => x.Image)
            .Must(BeAValidImage).WithMessage("Only JPG and PNG images are allowed.")
            .Must(f => f is not { Length: > 12 * 1024 * 1024 })
            .WithMessage("Image size must not exceed 12 MB.")
            .When(x => x.Image != null);

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

    private static bool BeAValidImage(IFormFile? file)
    {
        if (file == null) return true;
        var ext = Path.GetExtension(file.FileName).ToLower();
        return AllowedExtensions.Contains(ext);
    }
}