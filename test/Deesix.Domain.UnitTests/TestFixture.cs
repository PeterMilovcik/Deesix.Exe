using Microsoft.Extensions.Hosting;

namespace Deesix.Domain.UnitTests;

public class TestFixture
{
    protected IServiceProvider Services { get; private set; }

    [SetUp]
    public virtual void SetUp() => Services = CreateServices();

    private IServiceProvider CreateServices()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDeesixDomain();
            })
            .Build().Services;
    }
}
