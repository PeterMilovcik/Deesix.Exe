using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameOptions.CreateNewGame;

public class CanExecute : GameOptionTestFixture<CreateNewGameOption>
{
    [Test]
    public void Should_Return_True_When_Game_Has_No_Value() =>
        GameOption!.CanExecute(new GameTurn()).Should().BeTrue(
            because: "the game has no value.");
    
    [Test]
    public void Should_Return_False_When_Game_Has_Value() =>
        GameOption!.CanExecute(new GameTurn { Game = new Game()})
            .Should().BeFalse(
                because: "the game has already a value.");

    [Test]
    public void Should_Return_False_When_LastOption_Is_LoadGamesOption() => 
        GameOption!.CanExecute(
            new GameTurn { LastOption = new LoadGamesOption(GameRepository) })
                .Should().BeFalse(
                    because: "the last option is LoadGamesOption.");
}
