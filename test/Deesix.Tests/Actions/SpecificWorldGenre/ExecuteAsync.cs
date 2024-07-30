using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.Actions.SpecificWorldGenre;

public class ExecuteAsync : TestFixture
{
    private Game game;
    private const string Genre = "Test Genre";

    private IAction Action { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        Action = new SpecificWorldGenreAction(Genre, WorldRepository);
    }

    [Test]
    public async Task Should_Return_Turn_With_NewWorld() =>
        (await Action.ExecuteAsync(new Turn {Game = new Game()}))
            .Game.Value.World!.Genre.Should().Be(Genre, 
                because: "the world genre should be set to the genre");
    
    [Test]
    public async Task Should_Return_Turn_With_Message() =>
        (await Action.ExecuteAsync(new Turn{ Game = new Game()}))
            .Message.Should().Be($"World genre set to {Genre} Good choice!",
                because: "that is expected message");
    
    [Test]
    public async Task Should_Return_Turn_With_Question() =>
        (await Action.ExecuteAsync(new Turn{ Game = new Game()}))
            .Question.Should().Be("What would you like to do next?",
                because: "that is expected question");
    
    [Test]
    public async Task Should_Return_Turn_With_No_Actions() =>
        (await Action.ExecuteAsync(new Turn{ Game = new Game()}))
            .Actions.Should().BeEmpty(
                because: "there should be no actions");
}
