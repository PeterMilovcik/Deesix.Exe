using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.GameOptions;

public class GameOptionTestFixture<T> : TestFixture where T : IGameOption
{
    protected T? GameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        GameOption = Services
            .GetRequiredService<IEnumerable<IGameOption>>()
            .OfType<T>()
            .FirstOrDefault();
        GameOption.Should().NotBeNull(
            because: "it is registered as a service.");
    }
}
