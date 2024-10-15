using GuiderPro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuiderPro.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Place> Places { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
