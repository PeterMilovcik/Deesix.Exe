using Deesix.Application.Actions;
using FluentAssertions;

namespace Deesix.Tests.Actions.ExitGame;

public class Created : ActionTestFixture<ExitAction>
{
    [Test]
    public void Title_Should_Return_Start_New_Game() => 
        Action!.Title.Should().Be("Exit Game", because: "that is the expected title.");
    

    [Test]
    public void Order_Should_Return_IntMaxValue() => 
        Action!.Order.Should().Be(int.MaxValue, because: "that is the expected order.");
}
