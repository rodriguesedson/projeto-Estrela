namespace api.Models.Dtos;

public class EmailDto
{
    public EmailDto(string from, string to, string subject, string body)
    {
        From = from;
        To = to;
        Subject = subject;
        Body = body;
    }

    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}