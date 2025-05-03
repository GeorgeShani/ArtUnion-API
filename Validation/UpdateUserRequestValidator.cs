using FluentValidation;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Validation;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName))
            .WithMessage("First name must be 50 characters or fewer.");

        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName))
            .WithMessage("Last name must be 50 characters or fewer.");

        RuleFor(x => x.Username)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.Username))
            .WithMessage("Username must be 30 characters or fewer.");

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.Password))
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.Biography)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Biography))
            .WithMessage("Biography must be 500 characters or fewer.");

        RuleFor(x => x.ProfilePicture)
            .Must(BeAValidImage)
            .Must(file => file is not { Length: > 6 * 1024 * 1024 }) // 6MB max
            .When(x => x.ProfilePicture != null)
            .WithMessage("Profile picture must be a valid image file (jpg, jpeg, png).");
    }
    
    private static bool BeAValidImage(IFormFile? file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        return allowedTypes.Contains(file?.ContentType.ToLower());
    }
}