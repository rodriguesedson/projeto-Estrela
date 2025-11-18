using api.Models.Dtos;

namespace api.Interfaces;

public interface ISendEmailService
{
     void SendTestMessage(EmailDto email);
}