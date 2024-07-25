using Deesix.Application.GameOptions;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.GameOptions.CreateNewGameOptionTests;

public class Created : TestFixture
{
    private CreateNewGameOption? createNewGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        createNewGameOption = Services
            .GetRequiredService<IEnumerable<IGameOption>>()
            .OfType<CreateNewGameOption>()
            .FirstOrDefault();
        createNewGameOption.Should().NotBeNull(
            because: "it is registered as a service.");
    }

    [Test]
    public void Title_Should_Return_Create_New_Game() => 
        createNewGameOption!.Title.Should().Be("Create New Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        createNewGameOption!.Order.Should().Be(1, 
            because: "that is the expected order.");
}
