using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.LoadGames;

public class CanExecute : ActionTestFixture<LoadGamesAction>
{
    [Test]
    public void Should_Return_False_When_Game_HasValue() => 
        Action!.CanExecute(new Turn{ Game = new Game() })
            .Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void Should_Return_False_When_Game_HasNoValue_And_Repository_HasNoGames() => 
        Action!.CanExecute(new Turn())
            .Should().BeFalse(because: "there are no games in the game repository.");
    
    [Test]
    public void Should_Return_False_When_LastAction_Is_LoadGamesAction() => 
        Action!.CanExecute(new Turn{ LastAction = new LoadGamesAction(GameRepository) })
            .Should().BeFalse(because: $"the {nameof(Turn.LastAction)} is {nameof(LoadGamesAction)}.");

    [Test]
    public void Should_Return_True_When_Game_HasNoValue_And_GameRepository_HasSomeGame_And_LastAction_IsNull()
    {
        GameRepository.Add(new Game());
        GameRepository.SaveChanges();
        Action!.CanExecute(new Turn{ LastAction = null! })
            .Should().BeTrue(because: "there is no game yet, " + 
                "there are games in the game repository and " + 
                "the last game action is null.");
    }
}
