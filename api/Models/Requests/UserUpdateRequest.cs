namespace api.Models.Requests;

public class UserUpdateRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
}