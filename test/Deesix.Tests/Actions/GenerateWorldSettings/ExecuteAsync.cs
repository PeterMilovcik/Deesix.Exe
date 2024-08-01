using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.GenerateWorldSettings;

public class ExecuteAsync : ActionTestFixture<GenerateWorldSettingsAction>
{
    [Test, Explicit("OpenAI API call")]
    public async Task Should_Return_Turn_With_ExpectedProperties()
    {
        // Arrange
        var input = new Turn { Game = new Game { World = new World { Genre = "Test Genre" } } };
        // Act
        var turn = await Action!.ExecuteAsync(input);
        // Assert
        turn.Game.Value.World!.WorldSettings.Should().NotBeNull(
            because: "the turn has a game with a world that has genre and no world settings");
        turn.Message.Should().Contain("World Settings",
            because: "that is expected message after generating world settings");
        turn.Question.Should().Be("How do you want to proceed?",
            because: "that is expected question after generating world settings");
    }
}
