using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameActions.CreateNewGame;

public class CanExecute : GameActionTestFixture<CreateNewGameAction>
{
    [Test]
    public void Should_Return_True_When_Game_Has_No_Value() =>
        GameAction!.CanExecute(new GameTurn()).Should().BeTrue(
            because: "the game has no value.");
    
    [Test]
    public void Should_Return_False_When_Game_Has_Value() =>
        GameAction!.CanExecute(new GameTurn { Game = new Game()})
            .Should().BeFalse(
                because: "the game has already a value.");

    [Test]
    public void Should_Return_False_When_LastGameAction_Is_LoadGamesAction() => 
        GameAction!.CanExecute(
            new GameTurn { LastGameAction = new LoadGamesAction(GameRepository) })
                .Should().BeFalse(
                    because: $"the {nameof(GameTurn.LastGameAction)} is {nameof(LoadGamesAction)}.");
}
