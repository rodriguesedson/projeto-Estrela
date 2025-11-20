using api.Enums;

namespace api.Entities;

public class User
{
    public User(string email, string name, DateOnly birthDate)
    {
        Email = email;
        Name = name;
        BirthDate = birthDate;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
}