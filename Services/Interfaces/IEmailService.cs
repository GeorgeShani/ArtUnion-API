namespace ArtUnion_API.Services.Interfaces;

public interface IEmailService
{
    Task SendEmail(string to, string subject, string message);
}