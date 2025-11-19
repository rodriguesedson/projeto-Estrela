namespace api.Models.Requests;

public class UserRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
}