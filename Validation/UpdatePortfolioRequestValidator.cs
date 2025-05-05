using FluentValidation;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Validation;

public class UpdatePortfolioRequestValidator : AbstractValidator<UpdatePortfolioRequest>
{
    public UpdatePortfolioRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty when provided.")
            .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.")
            .When(x => x.Title != null);

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must be 1000 characters or fewer.")
            .When(x => x.Description != null);
    }
}