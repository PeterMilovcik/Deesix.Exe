using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions.LoadGame;

[TestFixture]
public class CanExecute : TestFixture
{
    private Game game;

    private IAction Action { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        Action = new LoadAction(game);
    }

    [Test]
    public void Should_Return_False_When_Turn_Has_Value() =>
        Action.CanExecute(new Turn { Game = new Game() })
            .Should().BeFalse(
                because: "this game action should only be executed when Turn has no value");
    
    [Test]
    public void Should_Return_False_When_LastAction_Is_Not_LoadGamesAction() =>
        Action.CanExecute(new Turn { Game = new Game(), LastAction = new Mock<IAction>().Object })
            .Should().BeFalse(
                because: "this game action should only be executed when LastAction is LoadGamesAction");
    
    [Test]
    public void Should_Return_True_When_Turn_Has_No_Game_And_LastAction_Is_LoadGamesAction() =>
        Action.CanExecute(new Turn { LastAction = new LoadGamesAction(GameRepository) })
            .Should().BeTrue(
                because: "this game action should only be executed when " + 
                "Turn has no game and LastAction is LoadGamesAction");
}
