using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.GameActions;

public class GameActionTestFixture<T> : TestFixture where T : IGameAction
{
    protected T? GameAction;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        GameAction = Services
            .GetRequiredService<IEnumerable<IGameAction>>()
            .OfType<T>()
            .FirstOrDefault();
        GameAction.Should().NotBeNull(
            because: "it is registered as a service.");
    }
}
