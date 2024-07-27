using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.GameActions.WorldGenres;

public class CanExecute : GameActionTestFixture<WorldGenresGameOption>
{
    [Test]
    public void Should_Return_False_When_GameTurn_Has_No_Game() =>
        GameAction!.CanExecute(new GameTurn())
            .Should().BeFalse(
                because: "this game action should only be executed when GameTurn has no value");
    
    [Test]
    public void Should_Return_False_When_Game_World_Is_Not_Null() =>
        GameAction!.CanExecute(new GameTurn { Game = new Game { World = new World() } })
            .Should().BeFalse(
                because: "this game action should only be executed when Game's World is null");
    
    [Test]
    public void Should_Return_False_When_LastGameAction_IsNot_CreateNewGameAction() => 
        GameAction!.CanExecute(new GameTurn { Game = new Game(), LastGameAction = new Mock<IGameAction>().Object })
            .Should().BeFalse(
                because: $"this game action should only be executed when LastGameAction is CreateNewGameAction");
    
    [Test]
    public void Should_Return_True_When_Correct_Conditions() =>
        GameAction!.CanExecute(new GameTurn 
            {
                Game = new Game(), 
                LastGameAction = new CreateNewGameAction(GameRepository) 
            })
            .Should().BeTrue(
                because: "this game action should only be executed when " +
                "GameTurn's Game has no World and LastGameAction is CreateNewGameAction");
}
