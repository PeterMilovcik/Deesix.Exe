using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Deesix.Tests;

public class TestFixture
{
    protected IServiceProvider Services { get; private set; }
    protected IRepository<Game> GameRepository { get; private set; }
    protected IRepository<World> WorldRepository { get; private set; }

    [SetUp]
    public virtual void SetUp()
    {
        Services = CreateServices();
        RecreateDatabase();
        GameRepository = Services.GetRequiredService<IRepository<Game>>();
        WorldRepository = Services.GetRequiredService<IRepository<World>>();
    }
    private static IServiceProvider CreateServices() => 
        Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDeesixConsoleUI();

                services.RemoveAll<ApplicationDbContext>();

                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlite($"Data Source=test.db"));
            })
            .Build().Services;

    private void RecreateDatabase()
    {
        var database = Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        database.EnsureDeleted();
        database.EnsureCreated();
    }

    [TearDown]
    public virtual void TearDown()
    {
        var database = Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        database.EnsureDeleted();
    }
}
