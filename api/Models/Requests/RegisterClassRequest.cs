using api.Entities;

namespace api.Models.Requests;

public class RegisterClassRequest
{
    public string Name { get; set; }
    public DayOfWeek[] Days { get; set; }
    public TimeOnly Time  { get; set; }
    public User[] Students { get; set; }
}