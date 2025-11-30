using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Contexts;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        base.OnModelCreating(builder);
    }
}