using System.Text.Json;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using VitakorProb.Interfaces;
using VitakorProb.Options;

namespace VitakorProb.Service;

public class MailerService : IMailer
{
    private readonly SmtpOptions _smtpOptions;

    public MailerService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string htmlBody)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_smtpOptions.SenderEmail));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

        using var smtp = new SmtpClient();
        
        await smtp.ConnectAsync(
            _smtpOptions.Server,
            int.Parse(_smtpOptions.Port), 
            SecureSocketOptions.SslOnConnect);

        await smtp.AuthenticateAsync(
            _smtpOptions.Username, 
            _smtpOptions.Password);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

