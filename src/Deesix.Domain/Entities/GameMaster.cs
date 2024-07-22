using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster : IGameMaster
{
    public GameTurn GameTurn { get; private set; }

    public GameMaster(IGameOptionFactory gameOptionFactory)
    {
        GameTurn = new GameTurn();
        GameTurn.GameOptions = gameOptionFactory.CreateGameOptions(GameTurn);
    }

    public async Task ProcessOptionAsync(IGameOption option) => GameTurn = await option.ExecuteAsync(GameTurn);
}
