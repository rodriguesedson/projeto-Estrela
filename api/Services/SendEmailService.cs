using System.Net;
using System.Net.Mail;
using api.Interfaces;
using api.Models.Dtos;

namespace api.Services;

public class SendEmailService : ISendEmailService
{
    private readonly IConfiguration _configuration;

    public SendEmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendTestMessage(EmailDto emailDto)
    {
        var credentials = _configuration.GetSection("Credentials");
        var from = new MailAddress(credentials["From"]);
        var to = new MailAddress(emailDto.To);
        
        MailMessage message = new MailMessage(from, to)
        {
            Subject = emailDto.Subject,
            Body = emailDto.Body,
        };
        
        var client = new SmtpClient
        {
            Host = credentials["Host"],
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(credentials["From"], credentials["Pass"]),
        };
        
        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send message: {0}", ex.Message);
        }
    }
}