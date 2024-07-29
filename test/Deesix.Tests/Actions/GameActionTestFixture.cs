using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.Actions;

public class ActionTestFixture<T> : TestFixture where T : IAction
{
    protected T? Action;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        Action = Services
            .GetRequiredService<IEnumerable<IAction>>()
            .OfType<T>()
            .FirstOrDefault();
        Action.Should().NotBeNull(
            because: "it is registered as a service.");
    }
}
