using Deesix.Application.Actions;
using FluentAssertions;

namespace Deesix.Tests.Actions.CreateNewGame;

public class Created : ActionTestFixture<CreateNewAction>
{
    [Test]
    public void Title_Should_Return_Create_New_Game() => 
        Action!.Title.Should().Be("Create New Game", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        Action!.Order.Should().Be(1, 
            because: "that is the expected order.");
}
