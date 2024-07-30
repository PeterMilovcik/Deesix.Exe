using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.Actions.SpecificWorldGenre;

public class CanExecute : TestFixture
{
    private Game game;
    private const string Genre = "High Fantasy";

    private IAction Action { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        Action = new SpecificWorldGenreAction(Genre, WorldRepository);
    }

    [Test]
    public void Should_Return_False_When_Turn_HasNoGame() =>
        Action.CanExecute(new Turn()).Should().BeFalse(
            because: "the turn has no game");

    [Test]
    public void Should_Return_False_When_Turn_AlreadyHasWorld() =>
        Action.CanExecute(new Turn{ Game = new Game { World = new World() }})
            .Should().BeFalse(
                because: "the turn has a game with a world");
    
    [Test]
    public void Should_Return_True_When_Turn_HasGame_ButNoWorld() =>
        Action.CanExecute(new Turn{ Game = new Game()})
            .Should().BeTrue(
                because: "the turn has a game but no world");
}
