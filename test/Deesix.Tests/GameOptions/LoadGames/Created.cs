using Deesix.Application.GameActions;
using FluentAssertions;

namespace Deesix.Tests.GameOptions.LoadGames;

public class Created : GameOptionTestFixture<LoadGamesOption>
{
    [Test]
    public void Title_Should_Return_Load_Game() => 
        GameOption!.Title.Should().Be("Load Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_2() => 
        GameOption!.Order.Should().Be(2, 
            because: "that is the expected order.");
}
