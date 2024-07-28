using Deesix.Application.GameActions;
using FluentAssertions;

namespace Deesix.Tests.GameActions.WorldGenres;

public class Created : GameActionTestFixture<WorldGenresGameOption>
{
    [Test]
    public void Should_Have_Correct_Title() => 
        GameAction!.Title.Should().Be("Choose a World Genre", 
            because: "that is the expected title");
    
    [Test]
    public void Should_Have_Correct_Order() =>
        GameAction!.Order.Should().Be(1, 
            because: "that is the expected order");
}
