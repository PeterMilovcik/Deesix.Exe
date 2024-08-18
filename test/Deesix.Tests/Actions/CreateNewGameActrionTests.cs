using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions;

public class CreateNewGameActrionTests : ActionTestFixture<CreateNewAction>
{
    protected override string ExpectedTitle => "Create new game";
    protected override string ExpectedProgressTitle => "Creating new game...";

    [Test]
    public void Should_Return_True_When_Game_Has_No_Value() =>
        Action!.CanExecute(new Turn()).Should().BeTrue(
            because: "the game has no value.");
    
    [Test]
    public void Should_Return_False_When_Game_Has_Value() =>
        Action!.CanExecute(new Turn { Game = new Game()})
            .Should().BeFalse(
                because: "the game has already a value.");

    [Test]
    public void Should_Return_False_When_LastAction_Is_LoadGamesAction() => 
        Action!.CanExecute(
            new Turn { LastAction = new LoadGamesAction(GameRepository) })
                .Should().BeFalse(
                    because: $"the {nameof(Turn.LastAction)} is {nameof(LoadGamesAction)}.");
    
    [Test]
    public async Task Should_Return_Turn_With_Empty_Actions() => 
        (await Action!.ExecuteAsync(new Turn())).Actions
            .Should().BeEmpty(
                because: $"{nameof(CreateNewAction)} doesn't create any new game actions.");

    [Test]
    public async Task Should_Return_Turn_With_Created_Game() => 
        (await Action!.ExecuteAsync(new Turn())).Game.HasValue
            .Should().BeTrue(
                because: "the game should be created during the execution.");

    [Test]
    public async Task Should_Return_Turn_With_Correct_Message() => 
        (await Action!.ExecuteAsync(new Turn())).Message
            .Should().Be("Game created successfully! Get ready for an exciting adventure!", 
                because: "that is the expected next message.");
}
