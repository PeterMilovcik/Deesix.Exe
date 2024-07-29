using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.WorldGenres;

public class CanExecute : ActionTestFixture<WorldGenresGameOption>
{
    [Test]
    public void Should_Return_False_When_Turn_Has_No_Game() =>
        Action!.CanExecute(new Turn())
            .Should().BeFalse(
                because: "this game action should only be executed when Turn has no value");
    
    [Test]
    public void Should_Return_False_When_Game_World_Is_Not_Null() =>
        Action!.CanExecute(new Turn { Game = new Game { World = new World() } })
            .Should().BeFalse(
                because: "this game action should only be executed when Game's World is null");
    
    [Test]
    public void Should_Return_False_When_LastAction_IsNot_CreateNewAction() => 
        Action!.CanExecute(new Turn { Game = new Game(), LastAction = new Mock<IAction>().Object })
            .Should().BeFalse(
                because: $"this game action should only be executed when LastAction is CreateNewAction");
    
    [Test]
    public void Should_Return_True_When_Correct_Conditions() =>
        Action!.CanExecute(new Turn 
            {
                Game = new Game(), 
                LastAction = new CreateNewAction(GameRepository) 
            })
            .Should().BeTrue(
                because: "this game action should only be executed when " +
                "Turn's Game has no World and LastAction is CreateNewAction");
}
