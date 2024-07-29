using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.Actions.LoadGame;

[TestFixture]
public class ExecuteAsync : TestFixture
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
    public async Task Should_Return_Turn_With_Correct_Message() =>
        (await Action.ExecuteAsync(new Turn()))
            .Message.Should().Be("Game loaded successfully.", 
                because: "that is the message that should be returned when the game is loaded");
    
    [Test]
    public async Task Should_Return_Turn_With_Correct_Game() =>
        (await Action.ExecuteAsync(new Turn()))
            .Game.Should().Be(game, 
                because: "this game action should load the game");
    
    [Test]
    public async Task Should_Return_Turn_With_Empty_Actions() =>
        (await Action.ExecuteAsync(new Turn()))
            .Actions.Should().BeEmpty(
                because: "this game action should not add any game actions");
}
