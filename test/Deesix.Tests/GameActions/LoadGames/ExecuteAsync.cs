using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.LoadGames;

public class ExecuteAsync : ActionTestFixture<LoadGamesAction>
{
    public override void SetUp()
    {
        base.SetUp();
        GameRepository.Add(new Game());
        GameRepository.SaveChanges();
    }

    [Test]
    public async Task Should_Not_Return_Turn_With_Empty_Actions() => 
        (await Action!.ExecuteAsync(new Turn())).Actions
            .Should().NotBeEmpty(because: $"{nameof(LoadGamesAction)} " + 
                $"should create new {nameof(LoadAction)}(s) for loading game(s).");
    
    [Test]
    public async Task Should_Return_Turn_With_Correct_Message() => 
        (await Action!.ExecuteAsync(new Turn())).Message
            .Should().Be("Please choose a game to play.", 
                because: "that is the expected next message.");
    
    [Test]
    public async Task Should_Return_Turn_With_Correct_Question() => 
        (await Action!.ExecuteAsync(new Turn())).Question
            .Should().Be("Which one would you like to play?", 
                because: "that is the expected next question.");
}
