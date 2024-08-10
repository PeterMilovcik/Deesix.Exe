using Deesix.Application;
using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.AcceptGeneratedWorldSettings;

public class CanExecute : ActionTestFixture<AcceptGeneratedWorldSettingsAction>
{
    [Test]
    public void Should_Return_True_When_Conditions_Are_Met_WithLastAction_GenerateWorldSettingsAction() =>
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
    public void Should_Return_True_When_Conditions_Are_Met_WithLastAction_RegenerateWorldSettingsAction() =>
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
    public void Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_No_WorldSettings() =>
        Action!.CanExecute(new Turn { Game = new Game { World = new World { Genre = "Test Genre" } } })
            .Should().BeFalse(
                because: "the turn has a game with a world and no world settings.");

    [Test]
    public void Should_Return_False_When_Turn_Has_Game_With_World_That_Has_No_Genre() =>
        Action!.CanExecute(new Turn { Game = new Game { World = new World() } })
            .Should().BeFalse(
                because: "the turn has a game with a world and no genre.");

    [Test]
    public void Should_Return_False_When_Turn_Has_Game_With_No_World() =>
        Action!.CanExecute(new Turn { Game = new Game() })
            .Should().BeFalse(
                because: "the turn has a game with no world.");

    [Test]
    public void Should_Return_False_When_Turn_Has_No_Game() =>
        Action!.CanExecute(new Turn())
            .Should().BeFalse(
                because: "the turn has no game.");
}
