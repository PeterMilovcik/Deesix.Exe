using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameActions.CreateNewGame;

public class ExecuteAsync : GameActionTestFixture<CreateNewGameAction>
{
    [Test]
    public async Task Should_Return_GameTurn_With_Empty_GameActions() => 
        (await GameAction!.ExecuteAsync(new GameTurn())).GameActions
            .Should().BeEmpty(
                because: $"{nameof(CreateNewGameAction)} doesn't create any new game actions.");

    [Test]
    public async Task Should_Return_GameTurn_With_Created_Game() => 
        (await GameAction!.ExecuteAsync(new GameTurn())).Game.HasValue
            .Should().BeTrue(
                because: "the game should be created during the execution.");

    [Test]
    public async Task Should_Return_GameTurn_With_Correct_Message() => 
        (await GameAction!.ExecuteAsync(new GameTurn())).Message
            .Should().Be("Game created successfully! Get ready for an exciting adventure!", 
                because: "that is the expected next message.");
}