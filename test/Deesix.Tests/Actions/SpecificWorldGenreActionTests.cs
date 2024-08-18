using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.Actions;

public class SpecificWorldGenreActionTests : ActionTestFixture
{
    private Game game;
    private const string Genre = "High Fantasy";
    protected override string ExpectedTitle => Genre;
    protected override string ExpectedProgressTitle => $"Setting world genre to {Genre}...";
    protected override IAction CreateAction() => new SpecificWorldGenreAction(Genre, WorldRepository);

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
    }

    [Test]
    public void CanExecute_Should_Return_False_When_Turn_HasNoGame() =>
        Action.CanExecute(new Turn()).Should().BeFalse(
            because: "the turn has no game");

    [Test]
    public void CanExecute_Should_Return_False_When_Turn_AlreadyHasWorld() =>
        Action.CanExecute(new Turn{ Game = new Game { World = new World() }})
            .Should().BeFalse(
                because: "the turn has a game with a world");
    
    [Test]
    public void CanExecute_Should_Return_True_When_Turn_HasGame_ButNoWorld() =>
        Action.CanExecute(new Turn{ Game = new Game()})
            .Should().BeTrue(
                because: "the turn has a game but no world");

    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_NewWorld() =>
        (await Action.ExecuteAsync(new Turn {Game = new Game()}))
            .Game.Value.World!.Genre.Should().Be(Genre, 
                because: "the world genre should be set to the genre");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Message() =>
        (await Action.ExecuteAsync(new Turn{ Game = new Game()}))
            .Message.Should().Be($"World genre set to {Genre} Good choice!",
                because: "that is expected message");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Question() =>
        (await Action.ExecuteAsync(new Turn{ Game = new Game()}))
            .Question.Should().Be("What would you like to do next?",
                because: "that is expected question");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_No_Actions() =>
        (await Action.ExecuteAsync(new Turn{ Game = new Game()}))
            .Actions.Should().BeEmpty(
                because: "there should be no actions");
}
