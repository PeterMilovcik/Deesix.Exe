using CSharpFunctionalExtensions;
using Deesix.Application.GameActions;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.GameMasterTests;

public class CreateNewGameWorkflow : GameMasterTestFixture
{
    [Test]
    public async Task Should_Be_Successful()
    {
        await Process<CreateNewGameAction>();
        await Process<WorldGenresGameOption>();
        await Process<SpecificWorldGenreGameAction>();
        GameMaster.GameTurn.Game.Value.World.Should().NotBeNull();
    }

    private async Task Process<TGameAction>() where TGameAction : IGameAction
    {
        GameMaster.GameTurn.ToConsole();
        Console.WriteLine($"Processing {typeof(TGameAction).Name}");
        await GameMaster.ProcessGameActionAsync(
            GameMaster.GameTurn.GameActions.OfType<TGameAction>().First());
        GameMaster.GameTurn.Game.ToConsole();
        GameMaster.GameTurn.Game.Execute(game => GameRepository.SaveChanges());
    }
}
