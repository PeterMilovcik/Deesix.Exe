using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Tests.TestDoubles;
using FluentAssertions;

namespace Deesix.Tests.Actions.GenerateWorldSettings;

public class CanExecute : ActionTestFixture<GenerateWorldSettingsAction>
{
    [Test]
    public void Should_Return_True_When_Turn_Has_Game_With_World_That_Has_Genre_And_No_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { World = new World { Genre = "Test Genre" } } })
            .Should().BeTrue(
                because: "the turn has a game with a world and no world settings.");
    
    [Test]
    public void Should_Return_False_When_Turn_Has_Game_With_World_That_Has_Genre_And_WorldSettings() => 
        Action!.CanExecute(new Turn { Game = new Game { World = new World { Genre = "Test Genre", WorldSettings = new SomeWorldSettings() } } })
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
