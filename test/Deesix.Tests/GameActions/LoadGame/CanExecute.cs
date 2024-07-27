using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.GameActions.LoadGame;

[TestFixture]
public class CanExecute : TestFixture
{
    private Game game;

    private IGameAction GameAction { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        GameAction = new LoadGameAction(game);
    }

    [Test]
    public void Should_Return_False_When_GameTurn_Has_Value() =>
        GameAction.CanExecute(new GameTurn { Game = new Game() })
            .Should().BeFalse(
                because: "this game action should only be executed when GameTurn has no value");
    
    [Test]
    public void Should_Return_False_When_LastGameAction_Is_Not_LoadGamesAction() =>
        GameAction.CanExecute(new GameTurn { Game = new Game(), LastGameAction = new Mock<IGameAction>().Object })
            .Should().BeFalse(
                because: "this game action should only be executed when LastGameAction is LoadGamesAction");
    
    [Test]
    public void Should_Return_True_When_GameTurn_Has_No_Game_And_LastGameAction_Is_LoadGamesAction() =>
        GameAction.CanExecute(new GameTurn { LastGameAction = new LoadGamesAction(GameRepository) })
            .Should().BeTrue(
                because: "this game action should only be executed when " + 
                "GameTurn has no game and LastGameAction is LoadGamesAction");
}
