using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.GenerateWorldSettings;

public class ExecuteAsync : ActionTestFixture<GenerateWorldSettingsAction>
{
    [Test, Explicit("OpenAI API call")]
    public async Task Should_Return_Turn_With_ExpectedProperties()
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
        turn.Game.Value.World!.WorldSettings.Should().NotBeNull(
            because: "the turn has a game with a world that has genre and no world settings");
        turn.Message.Should().Contain("World Settings",
            because: "that is expected message after generating world settings");
        turn.Question.Should().Be("How do you want to proceed?",
            because: "that is expected question after generating world settings");
        turn.Actions.Should().BeEmpty(
            because: "no actions are expected from generating world settings directly");
    }
}