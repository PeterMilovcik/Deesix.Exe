using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions;

public class LoadGamesActionTests : ActionTestFixture<LoadGamesAction>
{
    protected override string ExpectedTitle => "Load game";
    protected override string ExpectedProgressTitle => "Loading game...";
    protected override int ExpectedOrder => 2;

    public override void SetUp()
    {
        base.SetUp();
        GameRepository.Add(new Game());
        GameRepository.SaveChanges();
    }

    [Test]
    public void CanExecute_Should_Return_False_When_Game_HasValue() => 
        Action!.CanExecute(new Turn{ Game = new Game() })
            .Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void CanExecute_Should_Return_False_When_Game_HasNoValue_And_Repository_HasNoGames() => 
        Action!.CanExecute(new Turn())
            .Should().BeFalse(because: "there are no games in the game repository.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_LastAction_Is_LoadGamesAction() => 
        Action!.CanExecute(new Turn{ LastAction = new LoadGamesAction(GameRepository) })
            .Should().BeFalse(because: $"the {nameof(Turn.LastAction)} is {nameof(LoadGamesAction)}.");

    [Test]
    public void CanExecute_Should_Return_True_When_Game_HasNoValue_And_GameRepository_HasSomeGame_And_LastAction_IsNull()
    {
        GameRepository.Add(new Game());
        GameRepository.SaveChanges();
        Action!.CanExecute(new Turn{ LastAction = null! })
            .Should().BeTrue(because: "there is no game yet, " + 
                "there are games in the game repository and " + 
                "the last game action is null.");
    }

    [Test]
    public async Task ExecuteAsync_Should_Not_Return_Turn_With_Empty_Actions() => 
        (await Action!.ExecuteAsync(new Turn())).Actions
            .Should().NotBeEmpty(because: $"{nameof(LoadGamesAction)} " + 
                $"should create new {nameof(LoadGameAction)}(s) for loading game(s).");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Correct_Message() => 
        (await Action!.ExecuteAsync(new Turn())).Message
            .Should().Be("Please choose a game to play.", 
                because: "that is the expected next message.");
    
    [Test]
    public async Task ExecuteAsync_Should_Return_Turn_With_Correct_Question() => 
        (await Action!.ExecuteAsync(new Turn())).Question
            .Should().Be("Which one would you like to play?", 
                because: "that is the expected next question.");
}
