namespace api.Models.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Role { get; set; }
}