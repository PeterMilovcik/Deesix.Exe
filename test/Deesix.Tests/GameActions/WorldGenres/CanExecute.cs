using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Moq;

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
}

public class CanExecute : GameActionTestFixture<WorldGenresGameOption>
{
    [Test]
    public void Should_Return_False_When_GameTurn_Has_No_Game() =>
        GameAction!.CanExecute(new GameTurn())
            .Should().BeFalse(
                because: "this game action should only be executed when GameTurn has no value");
    
    [Test]
    public void Should_Return_False_When_Game_World_Is_Not_Null() =>
        GameAction!.CanExecute(new GameTurn { Game = new Game { World = new World() } })
            .Should().BeFalse(
                because: "this game action should only be executed when Game's World is null");
    
    [Test]
    public void Should_Return_False_When_LastGameAction_IsNot_CreateNewGameAction() => 
        GameAction!.CanExecute(new GameTurn { Game = new Game(), LastGameAction = new Mock<IGameAction>().Object })
            .Should().BeFalse(
                because: $"this game action should only be executed when LastGameAction is CreateNewGameAction");
    
    [Test]
    public void Should_Return_True_When_Correct_Conditions() =>
        GameAction!.CanExecute(new GameTurn 
            {
                Game = new Game(), 
                LastGameAction = new CreateNewGameAction(GameRepository) 
            })
            .Should().BeTrue(
                because: "this game action should only be executed when " +
                "GameTurn's Game has no World and LastGameAction is CreateNewGameAction");
}
