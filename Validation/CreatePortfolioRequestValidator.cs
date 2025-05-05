using FluentValidation;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Validation;

public class CreatePortfolioRequestValidator : AbstractValidator<CreatePortfolioRequest>
{
    public CreatePortfolioRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.ArtistId)
            .GreaterThan(0).WithMessage("ArtistId must be a positive number.");
    }
}
