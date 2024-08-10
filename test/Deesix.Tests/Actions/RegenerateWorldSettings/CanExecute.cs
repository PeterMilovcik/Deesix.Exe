using Deesix.Application;
using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.RegenerateWorldSettings;

public class CanExecute : ActionTestFixture<RegenerateWorldSettingsAction>
{
    [Test]
    public void Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_WorldSettings_With_LastAction_GenerateWorldSettingsAction() => 
        Action!.CanExecute(
            new Turn { Game = new Game { 
                World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } },
                    LastAction = new GenerateWorldSettingsAction(new Mock<IGenerator>().Object) })
                    .Should().BeTrue(
                        because: "the turn has a game with a world and no world settings and " + 
                            "last aciton is generate world settings action.");
    
    [Test]
    public void Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_WorldSettings_With_LastAction_RegenerateWorldSettingsAction() =>
        Action!.CanExecute(
            new Turn { Game = new Game { 
                World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } },
                    LastAction = new RegenerateWorldSettingsAction(new Mock<IGenerator>().Object) })
                    .Should().BeTrue(
                        because: "the turn has a game with a world and no world settingss and " + 
                            "last aciton is regenerate world settings action.");

    [Test]
    public void Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { 
            World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } } })
                .Should().BeFalse(
                    because: "the turn has a game with a world and world settings.");
    
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
