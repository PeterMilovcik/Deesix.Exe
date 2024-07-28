using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameActions.WorldGenres;

public class ExecuteAsync : GameActionTestFixture<WorldGenresGameOption>
{
    [Test]
    public async Task Should_Return_GameTurn_With_Message() =>
        (await GameAction!.ExecuteAsync(new GameTurn()))
            .Message.Should().Be("Let's choose a specific world genre.",
                because: "a message should be added to the GameTurn");

    [Test]
    public async Task Should_Return_GameTurn_With_Question() =>
        (await GameAction!.ExecuteAsync(new GameTurn()))
            .Question.Should().Be("Which one would you like to choose?",
                because: "a question should be added to the GameTurn");
    
    [Test]
    public async Task Should_Return_GameTurn_With_GameAction() =>
        (await GameAction!.ExecuteAsync(new GameTurn()))
            .GameActions.Should().NotBeEmpty(
                because: "a new game action should be added to the GameTurn");
    
    [Test]
    public async Task Should_Return_GameTurn_With_GameAction_With_Correct_Type() =>
        (await GameAction!.ExecuteAsync(new GameTurn()))
            .GameActions.First().Should().BeOfType<SpecificWorldGenreGameAction>(
                because: "a new game action should be added to the GameTurn");

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
    public async Task Should_Return_GameTurn_With_GameAction_With_Correct_Title(string genre)
    {
        var gameTurn = new GameTurn();
        gameTurn = await GameAction!.ExecuteAsync(gameTurn);        
        var gameAction = gameTurn.GameActions.FirstOrDefault(action => action.Title == genre);
        gameAction.Should().NotBeNull(
            because: "the specific world genre game action should be added to the GameTurn");
    }
}
