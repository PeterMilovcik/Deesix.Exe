using Deesix.Application;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.AcceptGeneratedWorldSettings;

public class ExecuteAsync : ActionTestFixture<AcceptGeneratedWorldSettingsAction>
{
    [Test]
    public async Task Should_Return_Empty_Actions()
    {
        // Arrange
        var input = new Turn 
        { 
            Game = new Game { World = new World { Genre = "Test Genre" } },
            Message = "Test Message",
            Question = "Test Question",
            Actions = new List<IAction> { new Mock<IAction>().Object }
        };
        // Act
        var turn = await Action!.ExecuteAsync(input);
        // Assert
        turn.Actions.Should().BeEmpty(
            because: "this action should not create any additional actions");
    }
}
