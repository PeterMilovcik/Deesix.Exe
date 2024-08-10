using Deesix.Application;
using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.RegenerateWorldSettings;

public class ExecuteAsync : ActionTestFixture<RegenerateWorldSettingsAction>
{
    [Test, Explicit("OpenAI API call")]
    public async Task Should_Return_Turn_With_ExpectedProperties()
    {
        // Arrange
        var input = new Turn 
        { 
            Game = new Game 
            { 
                World = new World 
                { 
                    Genre = "Test Genre",
                    WorldSettings = new SomeWorldSettings()
                }
            },
            Message = "Test Message",
            Question = "Test Question",
            Actions = new List<IAction> { new Mock<IAction>().Object },
            LastAction = new GenerateWorldSettingsAction(new Mock<IGenerator>().Object)
        };
        Action!.CanExecute(input).Should().BeTrue(
            because: "that is precondition for this test");
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