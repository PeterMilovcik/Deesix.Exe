using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster : IGameMaster
{
    public GameTurn GameTurn { get; private set; }

    public GameMaster(IEnumerable<IGameOption> initialGameOptions)
    {
        GameTurn = new GameTurn();
        GameTurn.GameOptions = initialGameOptions
            .Where(gameOption => gameOption.CanExecute(GameTurn))
            .OrderBy(gameOption => gameOption.Order).ToList();
    }

    public async Task ProcessOptionAsync(IGameOption option) => GameTurn = await option.ExecuteAsync(GameTurn);
}
