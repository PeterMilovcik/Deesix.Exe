using Deesix.Application.GameActions;
using FluentAssertions;

namespace Deesix.Tests.GameActions.ExitGame;

public class Created : GameActionTestFixture<ExitGameAction>
{
    [Test]
    public void Title_Should_Return_Start_New_Game() => 
        GameAction!.Title.Should().Be("Exit Game", because: "that is the expected title.");
    

    [Test]
    public void Order_Should_Return_IntMaxValue() => 
        GameAction!.Order.Should().Be(int.MaxValue, because: "that is the expected order.");
}
