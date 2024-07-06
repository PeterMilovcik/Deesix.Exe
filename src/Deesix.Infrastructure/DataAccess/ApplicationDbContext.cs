using Deesix.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deesix.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<World>().Property(b => b.WorldSettingsJson);
    }

    public DbSet<World> Worlds { get; set; }
}
