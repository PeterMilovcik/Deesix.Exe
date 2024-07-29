using Deesix.Application.Actions;
using FluentAssertions;

namespace Deesix.Tests.Actions.LoadGames;

public class Created : ActionTestFixture<LoadGamesAction>
{
    [Test]
    public void Title_Should_Return_Load_Game() => 
        Action!.Title.Should().Be("Load Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_2() => 
        Action!.Order.Should().Be(2, 
            because: "that is the expected order.");
}
