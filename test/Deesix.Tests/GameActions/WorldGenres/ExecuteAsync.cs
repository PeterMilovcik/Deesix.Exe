using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameActions.WorldGenres;

public class ExecuteAsync : GameActionTestFixture<WorldGenresGameOption>
{
    [Test]
    public async Task Should_Return_Turn_With_Message() =>
        (await GameAction!.ExecuteAsync(new Turn()))
            .Message.Should().Be("Let's choose a specific world genre.",
                because: "a message should be added to the Turn");

    [Test]
    public async Task Should_Return_Turn_With_Question() =>
        (await GameAction!.ExecuteAsync(new Turn()))
            .Question.Should().Be("Which one would you like to choose?",
                because: "a question should be added to the Turn");
    
    [Test]
    public async Task Should_Return_Turn_With_GameAction() =>
        (await GameAction!.ExecuteAsync(new Turn()))
            .GameActions.Should().NotBeEmpty(
                because: "a new game action should be added to the Turn");
    
    [Test]
    public async Task Should_Return_Turn_With_GameAction_With_Correct_Type() =>
        (await GameAction!.ExecuteAsync(new Turn()))
            .GameActions.First().Should().BeOfType<SpecificWorldGenreGameAction>(
                because: "a new game action should be added to the Turn");

    [TestCase("High Fantasy")]
    [TestCase("Low Fantasy")]
    [TestCase("Dystopian Fantasy")]
    [TestCase("Magical Realism")]
    [TestCase("Sword and Sorcery")]
    [TestCase("Urban Fantasy")]
    [TestCase("Paranormal Fantasy")]
    [TestCase("Dark Fantasy")]
    [TestCase("Superhero Fantasy")]
    [TestCase("Steampunk Fantasy")]
    [TestCase("Sci-fi Fantasy")]
    public async Task Should_Return_Turn_With_GameAction_With_Correct_Title(string genre)
    {
        var turn = new Turn();
        turn = await GameAction!.ExecuteAsync(turn);        
        var gameAction = turn.GameActions.FirstOrDefault(action => action.Title.StartsWith(genre));
        gameAction.Should().NotBeNull(
            because: "the specific world genre game action should be added to the Turn");
    }
}
