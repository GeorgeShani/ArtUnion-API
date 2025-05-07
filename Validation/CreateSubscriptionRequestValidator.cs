using FluentValidation;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Validation;

public class CreateSubscriptionRequestValidator : AbstractValidator<CreateSubscriptionRequest>
{
    public CreateSubscriptionRequestValidator()
    {
        RuleFor(x => x.SubscriberId)
            .GreaterThan(0).WithMessage("SubscriberId must be a positive integer.");

        RuleFor(x => x.ArtistId)
            .GreaterThan(0).WithMessage("ArtistId must be a positive integer.");

        RuleFor(x => x)
            .Must(x => x.SubscriberId != x.ArtistId)
            .WithMessage("A user cannot subscribe to themselves.");
    }
}