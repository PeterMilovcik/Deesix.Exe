using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.CreateNewGame;

public class CanExecute : ActionTestFixture<CreateNewAction>
{
    [Test]
    public void Should_Return_True_When_Game_Has_No_Value() =>
        Action!.CanExecute(new Turn()).Should().BeTrue(
            because: "the game has no value.");
    
    [Test]
    public void Should_Return_False_When_Game_Has_Value() =>
        Action!.CanExecute(new Turn { Game = new Game()})
            .Should().BeFalse(
                because: "the game has already a value.");

    [Test]
    public void Should_Return_False_When_LastAction_Is_LoadGamesAction() => 
        Action!.CanExecute(
            new Turn { LastAction = new LoadGamesAction(GameRepository) })
                .Should().BeFalse(
                    because: $"the {nameof(Turn.LastAction)} is {nameof(LoadGamesAction)}.");
}
