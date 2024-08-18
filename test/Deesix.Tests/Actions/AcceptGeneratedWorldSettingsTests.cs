using Deesix.Application;
using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions;

public class AcceptGeneratedWorldSettingsTests : ActionTestFixture<AcceptGeneratedWorldSettingsAction>
{
    protected override string ExpectedTitle => "Accept generated world settings";
    protected override string ExpectedProgressTitle => "Accepting generated world settings...";

    [Test]
    public void CanExecute_Should_Return_True_When_Conditions_Are_Met_WithLastAction_GenerateWorldSettingsAction() =>
        Action!.CanExecute(new Turn
        {
            Game = new Game
            {
                World = new World
                {
                    Genre = "Test Genre",
                    WorldSettings = new SomeWorldSettings()
                }
            },
            LastAction = new GenerateWorldSettingsAction(new Mock<IGenerator>().Object)
        }).Should().BeTrue(because: "all conditions are met.");
    
    [Test]
    public void CanExecute_Should_Return_True_When_Conditions_Are_Met_WithLastAction_RegenerateWorldSettingsAction() =>
        Action!.CanExecute(new Turn
        {
            Game = new Game
            {
                World = new World
                {
                    Genre = "Test Genre",
                    WorldSettings = new SomeWorldSettings()
                }
            },
            LastAction = new RegenerateWorldSettingsAction(new Mock<IGenerator>().Object)
        }).Should().BeTrue(because: "all conditions are met.");

    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_No_WorldSettings() =>
        Action!.CanExecute(new Turn { Game = new Game { World = new World { Genre = "Test Genre" } } })
            .Should().BeFalse(
                because: "the turn has a game with a world and no world settings.");

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
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Empty_Actions()
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
