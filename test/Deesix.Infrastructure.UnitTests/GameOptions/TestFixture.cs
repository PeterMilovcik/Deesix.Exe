using Microsoft.Extensions.Hosting;

namespace Deesix.Infrastructure.UnitTests;

public class TestFixture
{
    protected IServiceProvider Services { get; private set; }

    [SetUp]
    public virtual void SetUp() => Services = CreateServices();

    private static IServiceProvider CreateServices()
    {
        var testHost = Host.CreateDefaultBuilder()
                    .ConfigureServices((hostContext, services) => services.AddDeesixInfrastructure())
                    .Build();
        IServiceProvider services = testHost.Services;
        return services;
    }
}
