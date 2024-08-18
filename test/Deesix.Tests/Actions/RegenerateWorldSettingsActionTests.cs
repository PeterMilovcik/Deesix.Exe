using Deesix.Application;
using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions;

public class RegenerateWorldSettingsActionTests : ActionTestFixture<RegenerateWorldSettingsAction>
{
    protected override string ExpectedTitle => "Regenerate world settings";
    protected override string ExpectedProgressTitle => "Regenerating world settings...";
    protected override int ExpectedOrder => 2;

    [Test]
    public void CanExecute_Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_WorldSettings_With_LastAction_GenerateWorldSettingsAction() => 
        Action!.CanExecute(
            new Turn { Game = new Game { 
                World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } },
                    LastAction = new GenerateWorldSettingsAction(new Mock<IGenerator>().Object) })
                    .Should().BeTrue(
                        because: "the turn has a game with a world and no world settings and " + 
                            "last aciton is generate world settings action.");
    
    [Test]
    public void CanExecute_Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_WorldSettings_With_LastAction_RegenerateWorldSettingsAction() =>
        Action!.CanExecute(
            new Turn { Game = new Game { 
                World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } },
                    LastAction = new RegenerateWorldSettingsAction(new Mock<IGenerator>().Object) })
                    .Should().BeTrue(
                        because: "the turn has a game with a world and no world settingss and " + 
                            "last aciton is regenerate world settings action.");

    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { 
            World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } } })
                .Should().BeFalse(
                    because: "the turn has a game with a world and world settings.");
    
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
