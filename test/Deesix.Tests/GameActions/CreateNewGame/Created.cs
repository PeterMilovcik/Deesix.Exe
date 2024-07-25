using Deesix.Application.GameActions;
using FluentAssertions;

namespace Deesix.Tests.GameActions.CreateNewGame;

public class Created : GameActionTestFixture<CreateNewGameAction>
{
    [Test]
    public void Title_Should_Return_Create_New_Game() => 
        GameAction!.Title.Should().Be("Create New Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        GameAction!.Order.Should().Be(1, 
            because: "that is the expected order.");
}
