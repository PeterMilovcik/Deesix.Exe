using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameOptions.LoadGames;

public class ExecuteAsync : GameOptionTestFixture<LoadGamesOption>
{
    public override void SetUp()
    {
        base.SetUp();
        GameRepository.Add(new Game());
    }

    [Test]
    public async Task Should_Not_Return_GameTurn_With_Empty_GameOptions() => 
        (await GameOption!.ExecuteAsync(new GameTurn())).GameOptions
            .Should().NotBeEmpty(because: "this game option should create " + 
                "new specific game options for loading games.");
    
    [Test]
    public async Task Should_Return_GameTurn_With_Correct_Message() => 
        (await GameOption!.ExecuteAsync(new GameTurn())).Message
            .Should().Be("Please choose a game to play.", 
                because: "that is the expected next message.");
    
    [Test]
    public async Task Should_Return_GameTurn_With_Correct_Question() => 
        (await GameOption!.ExecuteAsync(new GameTurn())).Question
            .Should().Be("Which one would you like to play?", 
                because: "that is the expected next question.");
}
