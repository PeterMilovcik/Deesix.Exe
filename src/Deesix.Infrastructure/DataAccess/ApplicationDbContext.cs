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
        // Game
        modelBuilder.Entity<Game>().HasKey(g => g.GameId);

        // // Character
        // modelBuilder.Entity<Character>().HasKey(c => c.CharacterId);

        // // World
        // modelBuilder.Entity<World>().HasKey(w => w.WorldId);
        // modelBuilder.Entity<World>().Property(b => b.WorldSettingsJson);

        // // Realm
        // modelBuilder.Entity<Realm>().HasKey(r => r.RealmId);
        // modelBuilder.Entity<Realm>()
        //     .HasOne<World>()
        //     .WithMany(w => w.Realms)
        //     .HasForeignKey(r => r.WorldId);
    }

    public DbSet<Game> Games { get; set; }
    // public DbSet<Character> Characters { get; set; }
    // public DbSet<World> Worlds { get; set; }
    // public DbSet<Realm> Realms { get; set; }
}
