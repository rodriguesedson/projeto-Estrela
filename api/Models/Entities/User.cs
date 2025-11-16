using api.Enums;

namespace api.Entities;

public class User
{
    public User(string email, string name)
    {
        Email = email;
        Name = name;
    }

    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public Role Role { get; set; }
}