using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.GameActions.LoadGame;

[TestFixture]
public class ExecuteAsync : TestFixture
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
    public async Task Should_Return_GameTurn_With_Correct_Message() =>
        (await GameAction.ExecuteAsync(new GameTurn()))
            .Message.Should().Be("Game loaded successfully.", 
                because: "that is the message that should be returned when the game is loaded");
    
    [Test]
    public async Task Should_Return_GameTurn_With_Correct_Game() =>
        (await GameAction.ExecuteAsync(new GameTurn()))
            .Game.Should().Be(game, 
                because: "this game action should load the game");
    
    [Test]
    public async Task Should_Return_GameTurn_With_Empty_GameActions() =>
        (await GameAction.ExecuteAsync(new GameTurn()))
            .GameActions.Should().BeEmpty(
                because: "this game action should not add any game actions");
}
