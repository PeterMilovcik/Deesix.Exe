using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace Deesix.Application.UnitTests;

public class TestFixture
{
    protected IServiceProvider Services { get; private set; }
    protected Mock<IRepository<Game>> GameRepositoryMock { get; private set; } = null!;
    protected Mock<IRepository<World>> WorldRepositoryMock { get; private set; } = null!;

    [SetUp]
    public virtual void SetUp() => Services = CreateServices();

    private IServiceProvider CreateServices()
    {
        GameRepositoryMock = new Mock<IRepository<Game>>();
        WorldRepositoryMock = new Mock<IRepository<World>>();
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDeesixApplication();
                services.AddSingleton(GameRepositoryMock.Object);
                services.AddSingleton(WorldRepositoryMock.Object);
            })
            .Build().Services;
    }
}

