using FluentValidation;
using ArtUnion_API.Requests.PUT;

namespace ArtUnion_API.Validation;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        // Make all fields optional by default (not required)
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty when provided.")
            .MaximumLength(50).WithMessage("First name must be 50 characters or fewer.")
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty when provided.")
            .MaximumLength(50).WithMessage("Last name must be 50 characters or fewer.")
            .When(x => x.LastName != null);

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty when provided.")
            .MaximumLength(30).WithMessage("Username must be 30 characters or fewer.")
            .When(x => x.Username != null);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty when provided.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
            .When(x => x.Password != null);

        RuleFor(x => x.Biography)
            .MaximumLength(500).WithMessage("Biography must be 500 characters or fewer.")
            .When(x => x.Biography != null);

        RuleFor(x => x.ProfilePicture)
            .Must(BeAValidImage)
            .WithMessage("Profile picture must be a valid image file (jpg, jpeg, png).")
            .Must(file => file is not { Length: > 6 * 1024 * 1024 })
            .WithMessage("Profile picture must be 6MB or smaller.")
            .When(x => x.ProfilePicture != null);
    }
    
    private static bool BeAValidImage(IFormFile? file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        return allowedTypes.Contains(file?.ContentType.ToLower());
    }
}