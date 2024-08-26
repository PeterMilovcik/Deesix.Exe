using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Deesix.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestKitLibrary;

namespace Deesix.Tests;

[SetUpFixture]
public class DeesixTestKitSetUpFixture : AbstractTestKitSetUpFixture
{
    [OneTimeSetUp]
    public override void OneTimeSetUp()
    {
        base.OneTimeSetUp();

        var database = TestKit.Get<IServiceProvider>()
            .CreateScope().ServiceProvider
            .GetRequiredService<ApplicationDbContext>().Database;
        database.EnsureDeleted();
        database.EnsureCreated();
    }

    protected override void Configure(TestKitConfiguration config)
    {
        config.AddTestStep();
        config.AddDevelopmentAspNetCoreEnvironment();
        config.AddProcessAction();
        var services = CreateServices();
        config.Add(services);
        config.Add(services.CreateScope().ServiceProvider.GetRequiredService<IRepository<Game>>());
        config.Add(services.CreateScope().ServiceProvider.GetRequiredService<IRepository<World>>());
        config.Add(services.CreateScope().ServiceProvider.GetRequiredService<IGameMaster>());
    }

    private IServiceProvider CreateServices() => 
        Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDeesixConsoleUI(hostContext.Configuration);
            })
            .Build().Services;

    [OneTimeTearDown]
    public override void OneTimeTearDown() => base.OneTimeTearDown();
}
