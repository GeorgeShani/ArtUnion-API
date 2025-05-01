namespace ArtUnion_API.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string message);
}