using Deesix.Application;
using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions;

public class GenerateWorldDescriptionTests : ActionTestFixture<GenerateWorldDescriptionAction>
{
    protected override string ExpectedTitle => "Generate world description";
    protected override string ExpectedProgressTitle => "Generating world description...";
    
    [Test]
    public void CanExecute_Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_And_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { 
            World = new World { Genre = "Dummy Genre", WorldSettings = new SomeWorldSettings(), Description = null } },
            LastAction = new AcceptGeneratedWorldSettingsAction() })
                .Should().BeTrue(
                    because: "the turn has a game with a world, world settings and no description and " + 
                        "the last action was to accept the generated world settings.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_WorldSettings_ButHasDescription() => 
        Action!.CanExecute(new Turn { Game = new Game { 
            World = new World { Genre = "Dummy Genre", WorldSettings = new SomeWorldSettings(), Description = "Dummy Description" } } })
            .Should().BeFalse(
                because: "the turn has a game with a world, world settings and it already has a description.");
    
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
                    Genre = "High Fantasy", 
                    WorldSettings = new SomeWorldSettings() 
                },
            },
            Message = "Test Message",
            Question = "Test Question",
            LastAction = new AcceptGeneratedWorldSettingsAction(),
            Actions = new List<IAction> { new Mock<IAction>().Object }
        };
        Action!.CanExecute(input).Should().BeTrue(because: "that is the test precondition.");
        // Act
        var turn = await Action!.ExecuteAsync(input);
        // Assert
        turn.Actions.Should().NotBeEmpty();
        turn.Message.Should().Be("Please choose a description for the world.");
        turn.Question.Should().Be("Which description do you want to use?");
    }
}
