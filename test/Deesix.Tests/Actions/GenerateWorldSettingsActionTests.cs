using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions;

public class GenerateWorldSettingsActionTests : ActionTestFixture<GenerateWorldSettingsAction>
{
    protected override string ExpectedTitle => "Generate world settings";
    protected override string ExpectedProgressTitle => "Generating world settings...";

    [Test]
    public void CanExecute_Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_AndNo_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { 
            World = new World { Genre = "Dummy Genre" } } })
                .Should().BeTrue(
                    because: "the turn has a game with a world and no world settings.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { 
            World = new World { Genre = "Dummy Genre", WorldSettings = new SomeWorldSettings() } } })
            .Should().BeFalse(
                because: "the turn has a game with a world and it already has a world settings.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Game_With_World_That_Has_No_Genre() => 
        Action!.CanExecute(new Turn { Game = new Game { World = new World() } })
            .Should().BeFalse(
                because: "the turn has a game with a world and no genre.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Game_With_No_World() => 
        Action!.CanExecute(new Turn { Game = new Game() })
            .Should().BeFalse(
                because: "the turn has a game with no world.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_No_Game() => 
        Action!.CanExecute(new Turn())
            .Should().BeFalse(
                because: "the turn has no game.");
    
    [Test, Explicit("OpenAI API call")]
    public async Task ExecuteAsync_Should_Return_Turn_With_ExpectedProperties()
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
