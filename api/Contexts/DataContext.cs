using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Contexts;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ProjectClass> ProjectClasses { get; set; } = null!;
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(user => user.Email).IsUnique();
        builder.Entity<ProjectClass>().HasIndex(projectClass => projectClass.Name).IsUnique();
        base.OnModelCreating(builder);
    }
}