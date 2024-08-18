using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Deesix.Tests.Actions;

public class WorldGenresActionTests : ActionTestFixture<WorldGenresAction>
{
    protected override string ExpectedTitle => "Choose a world genre";
    protected override string ExpectedProgressTitle => "Choosing a world genre...";
    
    [Test]
    public void CanExecute_Should_Return_False_When_Turn_Has_No_Game() =>
        Action!.CanExecute(new Turn())
            .Should().BeFalse(
                because: "this game action should only be executed when Turn has no value");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Game_World_Is_Not_Null() =>
        Action!.CanExecute(new Turn { Game = new Game { World = new World() } })
            .Should().BeFalse(
                because: "this game action should only be executed when Game's World is null");
    
    [Test]
    public void CanExecute_Should_Return_False_When_LastAction_IsNot_CreateNewAction() => 
        Action!.CanExecute(new Turn { Game = new Game(), LastAction = new Mock<IAction>().Object })
            .Should().BeFalse(
                because: $"this game action should only be executed when LastAction is CreateNewAction");
    
    [Test]
    public void CanExecute_Should_Return_True_When_Correct_Conditions() =>
        Action!.CanExecute(new Turn 
            {
                Game = new Game(), 
                LastAction = new CreateNewAction(GameRepository) 
            })
            .Should().BeTrue(
                because: "this game action should only be executed when " +
                "Turn's Game has no World and LastAction is CreateNewAction");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Message() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Message.Should().Be("Let's choose a specific world genre.",
                because: "a message should be added to the Turn");

    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Question() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Question.Should().Be("Which one would you like to choose?",
                because: "a question should be added to the Turn");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Action() =>
        (await Action!.ExecuteAsync(new Turn()))
            .Actions.Should().NotBeEmpty(
                because: "a new game action should be added to the Turn");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Action_With_Correct_Type() =>
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
    public async Task ExecuteAsync_Should_Return_Turn_With_Action_With_Correct_Title(string genre)
    {
        var turn = new Turn();
        turn = await Action!.ExecuteAsync(turn);        
        var action = turn.Actions.FirstOrDefault(action => action.Title.StartsWith(genre));
        action.Should().NotBeNull(
            because: "the specific world genre game action should be added to the Turn");
    }
}
