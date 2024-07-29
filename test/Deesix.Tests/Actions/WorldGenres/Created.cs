using Deesix.Application.Actions;
using FluentAssertions;

namespace Deesix.Tests.Actions.WorldGenres;

public class Created : ActionTestFixture<WorldGenresAction>
{
    [Test]
    public void Should_Have_Correct_Title() => 
        Action!.Title.Should().Be("Choose a World Genre", 
            because: "that is the expected title");
    
    [Test]
    public void Should_Have_Correct_Order() =>
        Action!.Order.Should().Be(1, 
            because: "that is the expected order");
}
