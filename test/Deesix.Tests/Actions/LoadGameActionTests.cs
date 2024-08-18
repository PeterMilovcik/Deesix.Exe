using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions;

[TestFixture]
public class LoadGameActionTests : ActionTestFixture
{
    private Game? game;
    protected override string ExpectedTitle => $"Load: {game!.GameId} - Unknown World";
    protected override string ExpectedProgressTitle => "Loading game...";
    
    protected override IAction CreateAction()
    {
        game = new Game();
        GameRepository.Add(game);
        return new LoadGameAction(game);
    }

    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_Value() =>
        Action!.CanExecute(new Turn { Game = new Game() })
            .Should().BeFalse(
                because: "this game action should only be executed when Turn has no value");
    
    [Test]
    public void CanExecute_Should_Return_False_When_LastAction_Is_Not_LoadGamesAction() =>
        Action!.CanExecute(new Turn { Game = new Game(), LastAction = new Mock<IAction>().Object })
            .Should().BeFalse(
                because: "this game action should only be executed when LastAction is LoadGamesAction");
    
    [Test]
    public void CanExecute_Should_Return_True_When_Turn_Has_No_Game_And_LastAction_Is_LoadGamesAction() =>
        Action!.CanExecute(new Turn { LastAction = new LoadGamesAction(GameRepository) })
            .Should().BeTrue(
                because: "this game action should only be executed when " + 
                "Turn has no game and LastAction is LoadGamesAction");

    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Correct_Message() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Message.Should().Be("Game loaded successfully.", 
                because: "that is the message that should be returned when the game is loaded");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Correct_Game() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Game.Should().Be(game, 
                because: "this game action should load the game");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Empty_Actions() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Actions.Should().BeEmpty(
                because: "this game action should not add any game actions");
}
