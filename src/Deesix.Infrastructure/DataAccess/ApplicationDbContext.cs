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
        modelBuilder.Entity<World>().HasKey(w => w.WorldId);
        modelBuilder.Entity<World>().Property(b => b.WorldSettingsJson);
        modelBuilder.Entity<World>().Property(w => w.WorldId).ValueGeneratedOnAdd();

        modelBuilder.Entity<Realm>()
            .HasOne<World>()
            .WithMany(w => w.Realms)
            .HasForeignKey(r => r.WorldId);
    }

    public DbSet<World> Worlds { get; set; }
    public DbSet<Realm> Realms { get; set; }
}
