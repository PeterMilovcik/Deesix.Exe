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
        var options = gameOptionFactory
            .CreateGameOptions(GameTurn)
            .Where(option => option.CanExecute(GameTurn));
        GameTurn.GameOptions.AddRange(options);
    }

    public async Task ProcessOptionAsync(IGameOption option)
    {
        GameTurn = await option.ExecuteAsync(GameTurn);
        GameTurn.LastOption = option;
        var generalGameOptions = gameOptionFactory
            .CreateGameOptions(GameTurn)
            .Where(option => option.CanExecute(GameTurn));
        GameTurn.GameOptions.AddRange(generalGameOptions);
    }
}
