using FluentValidation;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Validation;

public class UpdateCritiqueRequestValidator : AbstractValidator<UpdateCritiqueRequest>
{
    public UpdateCritiqueRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(0.0, 5.0)
            .WithMessage("Rating must be between 0.0 and 5.0")
            .When(x => x.Rating != null);
        
        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage("Comment must not exceed 1000 characters")
            .When(x => x.Comment != null);
    }   
}