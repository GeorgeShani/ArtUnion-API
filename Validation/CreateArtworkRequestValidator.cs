using FluentValidation;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Validation;

public class CreateArtworkRequestValidator : AbstractValidator<CreateArtworkRequest>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];

    public CreateArtworkRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image is required.")
            .Must(BeAValidImage).WithMessage("Only JPG and PNG images are allowed.")
            .Must(f => f.Length <= 12 * 1024 * 1024) // 12MB
            .WithMessage("Image size must not exceed 12 MB.");

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

    private static bool BeAValidImage(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();
        return AllowedExtensions.Contains(extension);
    }
}