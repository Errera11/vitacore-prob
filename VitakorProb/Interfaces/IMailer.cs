namespace VitakorProb.Interfaces;

public interface IMailer
{
    Task SendEmailAsync(string email, string subject, string message);
}