using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
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
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        Services = CreateServices();
        RecreateDatabase();
        GameRepository = Services.GetRequiredService<IRepository<Game>>();
        WorldRepository = Services.GetRequiredService<IRepository<World>>();
    }

    private IServiceProvider CreateServices() => 
        Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDeesixConsoleUI();
            })
            .Build().Services;

    private void RecreateDatabase()
    {
        var database = Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        database.EnsureDeleted();
        database.EnsureCreated();
    }
}
