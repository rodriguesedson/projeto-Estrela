using api.Enums;

namespace api.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
}