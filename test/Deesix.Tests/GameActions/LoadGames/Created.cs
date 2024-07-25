using Deesix.Application.GameActions;
using FluentAssertions;

namespace Deesix.Tests.GameActions.LoadGames;

public class Created : GameActionTestFixture<LoadGamesAction>
{
    [Test]
    public void Title_Should_Return_Load_Game() => 
        GameAction!.Title.Should().Be("Load Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_2() => 
        GameAction!.Order.Should().Be(2, 
            because: "that is the expected order.");
}
