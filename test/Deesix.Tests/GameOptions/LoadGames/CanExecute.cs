using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameOptions.LoadGames;

public class CanExecute : GameOptionTestFixture<LoadGamesOption>
{
    [Test]
    public void Should_Return_False_When_Game_HasValue() => 
        GameOption!.CanExecute(new GameTurn{ Game = new Game() })
            .Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void Should_Return_False_When_Game_HasNoValue_And_Repository_HasNoGames() => 
        GameOption!.CanExecute(new GameTurn())
            .Should().BeFalse(because: "there are no games in the repository.");
    
    [Test]
    public void Should_Return_False_When_LastOption_Is_LoadGamesOption() => 
        GameOption!.CanExecute(new GameTurn{ LastOption = new LoadGamesOption(GameRepository) })
            .Should().BeFalse(because: "the last option is LoadGamesOption.");

    [Test]
    public void Should_Return_True_When_Game_HasNoValue_And_GameRepository_HasSomeGame_And_LastOption_IsNull()
    {
        GameRepository.Add(new Game());
        GameOption!.CanExecute(new GameTurn{ LastOption = null! })
            .Should().BeTrue(because: "there is some game in the repository.");
    }
}
