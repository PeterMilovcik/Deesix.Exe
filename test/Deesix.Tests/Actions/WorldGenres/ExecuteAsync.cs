using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.WorldGenres;

public class ExecuteAsync : ActionTestFixture<WorldGenresAction>
{
    [Test]
    public async Task Should_Return_Turn_With_Message() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Message.Should().Be("Let's choose a specific world genre.",
                because: "a message should be added to the Turn");

    [Test]
    public async Task Should_Return_Turn_With_Question() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Question.Should().Be("Which one would you like to choose?",
                because: "a question should be added to the Turn");
    
    [Test]
    public async Task Should_Return_Turn_With_Action() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Actions.Should().NotBeEmpty(
                because: "a new game action should be added to the Turn");
    
    [Test]
    public async Task Should_Return_Turn_With_Action_With_Correct_Type() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Actions.First().Should().BeOfType<SpecificWorldGenreAction>(
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
    public async Task Should_Return_Turn_With_Action_With_Correct_Title(string genre)
    {
        var turn = new Turn();
        turn = await Action!.ExecuteAsync(turn);        
        var action = turn.Actions.FirstOrDefault(action => action.Title.StartsWith(genre));
        action.Should().NotBeNull(
            because: "the specific world genre game action should be added to the Turn");
    }
}
