using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.CreateNewGame;

public class ExecuteAsync : ActionTestFixture<CreateNewAction>
{
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