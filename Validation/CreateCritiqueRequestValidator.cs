using FluentValidation;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Validation;

public class CreateCritiqueRequestValidator : AbstractValidator<CreateCritiqueRequest>
{
    public CreateCritiqueRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(0.0, 5.0)
            .WithMessage("Rating must be between 0.0 and 5.0");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required")
            .MaximumLength(1000).WithMessage("Comment must not exceed 1000 characters");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("CreatedAt cannot be in the future");

        RuleFor(x => x.ArtworkId)
            .GreaterThan(0).WithMessage("ArtworkId must be a positive integer");

        RuleFor(x => x.CriticId)
            .GreaterThan(0).WithMessage("CriticId must be a positive integer");
    }
}