using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameOptions.CreateNewGame;

public class ExecuteAsync : GameOptionTestFixture<CreateNewGameOption>
{
    [Test]
    public async Task Should_Return_GameTurn_With_Empty_GameOptions() => 
        (await GameOption!.ExecuteAsync(new GameTurn())).GameOptions
            .Should().BeEmpty(
                because: "this game option doesn't create any new game options.");

    [Test]
    public async Task Should_Return_GameTurn_With_Created_Game() => 
        (await GameOption!.ExecuteAsync(new GameTurn())).Game.HasValue
            .Should().BeTrue(
                because: "the game should be created during the execution.");

    [Test]
    public async Task Should_Return_GameTurn_With_Correct_Message() => 
        (await GameOption!.ExecuteAsync(new GameTurn())).Message
            .Should().Be("Game created successfully! Get ready for an exciting adventure!", 
                because: "that is the expected next message.");
}