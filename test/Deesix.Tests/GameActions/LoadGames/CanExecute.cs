using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameActions.LoadGames;

public class CanExecute : GameActionTestFixture<LoadGamesAction>
{
    [Test]
    public void Should_Return_False_When_Game_HasValue() => 
        GameAction!.CanExecute(new Turn{ Game = new Game() })
            .Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void Should_Return_False_When_Game_HasNoValue_And_Repository_HasNoGames() => 
        GameAction!.CanExecute(new Turn())
            .Should().BeFalse(because: "there are no games in the game repository.");
    
    [Test]
    public void Should_Return_False_When_LastGameAction_Is_LoadGamesAction() => 
        GameAction!.CanExecute(new Turn{ LastGameAction = new LoadGamesAction(GameRepository) })
            .Should().BeFalse(because: $"the {nameof(Turn.LastGameAction)} is {nameof(LoadGamesAction)}.");

    [Test]
    public void Should_Return_True_When_Game_HasNoValue_And_GameRepository_HasSomeGame_And_LastGameAction_IsNull()
    {
        GameRepository.Add(new Game());
        GameRepository.SaveChanges();
        GameAction!.CanExecute(new Turn{ LastGameAction = null! })
            .Should().BeTrue(because: "there is no game yet, " + 
                "there are games in the game repository and " + 
                "the last game action is null.");
    }
}
