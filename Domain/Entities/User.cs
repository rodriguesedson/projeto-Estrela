using api.Enums;

namespace api.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public string? TempPasswordHash { get; set; }
    public DateTime? TempPasswordExpiresAt { get; set; }
    public bool TempPasswordUsed { get; set; }
}