using System.Net;
using System.Net.Mail;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class EmailService : IEmailService
{
    private readonly int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!);
    private readonly string smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST")!;
    private readonly string smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME")!;
    private readonly string smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")!;
    private readonly string smtpDisplayName = Environment.GetEnvironmentVariable("SMTP_DISPLAY_NAME")!;
    
    private readonly SmtpClient _smtpClient;

    public EmailService()
    {
        _smtpClient = new SmtpClient(smtpHost)
        {
            Port = smtpPort,
            EnableSsl = true,
            Credentials = new NetworkCredential(smtpUsername, smtpPassword)
        };
    }

    public async Task SendEmailAsync(string to, string subject, string message)
    {
        using var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(smtpUsername, smtpDisplayName);
        mailMessage.Subject = subject;
        mailMessage.Body = message;
        mailMessage.IsBodyHtml = true;

        mailMessage.To.Add(to);
        await _smtpClient.SendMailAsync(mailMessage);
    }
}