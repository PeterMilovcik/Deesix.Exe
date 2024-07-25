using Deesix.Application.GameActions;
using FluentAssertions;

namespace Deesix.Tests.GameOptions.CreateNewGame;

public class Created : GameOptionTestFixture<CreateNewGameOption>
{
    [Test]
    public void Title_Should_Return_Create_New_Game() => 
        GameOption!.Title.Should().Be("Create New Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        GameOption!.Order.Should().Be(1, 
            because: "that is the expected order.");
}
