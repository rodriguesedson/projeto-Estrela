namespace api.Models.Requests;

public class UserUpdateRequestDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
}