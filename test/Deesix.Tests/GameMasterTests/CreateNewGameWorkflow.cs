using Deesix.Application.GameActions;
using Deesix.Domain.Interfaces;

namespace Deesix.Tests.GameMasterTests;

public class CreateNewGameWorkflow : GameMasterTestFixture
{
    [Test]
    public async Task Should_Be_Successful()
    {
        await ProcessGameActionAsync<CreateNewGameAction>();
        await ProcessGameActionAsync<WorldGenresGameOption>();
        await ProcessGameActionAsync<SpecificWorldGenreGameAction>();
    }

    private async Task ProcessGameActionAsync<TGameAction>() where TGameAction : IGameAction
    {
        GameMaster.GameTurn.ToConsole();
        Console.WriteLine($"Processing {typeof(TGameAction).Name}");
        await GameMaster.ProcessGameActionAsync(
            GameMaster.GameTurn.GameActions.OfType<TGameAction>().First());
        GameMaster.GameTurn.Game.ToConsole();
    }
}
