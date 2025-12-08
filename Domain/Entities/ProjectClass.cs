namespace api.Entities;

public class ProjectClass
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Name { get; set; }
    public DayOfWeek[] Days { get; set; }
    public TimeOnly Time  { get; set; }
    public User[] Students { get; set; }
}