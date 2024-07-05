using Deesix.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deesix.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<World> Worlds { get; set; }
}