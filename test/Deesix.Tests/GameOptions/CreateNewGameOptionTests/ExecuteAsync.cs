using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.GameOptions.CreateNewGameOptionTests;

public class ExecuteAsync : TestFixture
{
    private CreateNewGameOption? createNewGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        createNewGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<CreateNewGameOption>().FirstOrDefault();
        createNewGameOption.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public async Task Should_Return_GameTurn_With_Empty_GameOptions() => 
        (await createNewGameOption!.ExecuteAsync(new GameTurn())).GameOptions
            .Should().BeEmpty(
                because: "this game option doesn't create any new game options.");

    [Test]
    public async Task Should_Return_GameTurn_With_Created_Game() => 
        (await createNewGameOption!.ExecuteAsync(new GameTurn())).Game.HasValue
            .Should().BeTrue(
                because: "the game should be created during the execution.");

    [Test]
    public async Task Should_Return_GameTurn_With_Correct_Message() => 
        (await createNewGameOption!.ExecuteAsync(new GameTurn())).Message
            .Should().Be("Game created successfully! Get ready for an exciting adventure!", 
                because: "that is the expected next message.");
}