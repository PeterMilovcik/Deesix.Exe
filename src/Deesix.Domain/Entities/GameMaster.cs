using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster : IGameMaster
{
    public GameTurn GameTurn { get; private set; }
    private readonly IGameOptionFactory gameOptionFactory;

    public GameMaster(IGameOptionFactory gameOptionFactory)
    {
        GameTurn = new GameTurn();
        this.gameOptionFactory = gameOptionFactory;
        GameTurn.GameOptions.AddRange(gameOptionFactory.CreateGameOptions(GameTurn));
    }

    public async Task ProcessOptionAsync(IGameOption option)
    {
        GameTurn = await option.ExecuteAsync(GameTurn);
        GameTurn.LastOption = option;
        GameTurn.GameOptions.AddRange(gameOptionFactory.CreateGameOptions(GameTurn));
    }
}
