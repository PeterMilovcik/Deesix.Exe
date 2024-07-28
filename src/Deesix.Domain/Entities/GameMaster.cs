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
        GameTurn.GameActions.AddRange(options);
    }

    public async Task ProcessGameActionAsync(IGameAction option)
    {
        GameTurn = await option.ExecuteAsync(GameTurn);
        GameTurn.LastGameAction = option;
        var generalGameOptions = gameOptionFactory
            .CreateGameOptions(GameTurn)
            .Where(option => option.CanExecute(GameTurn));
        GameTurn.GameActions.AddRange(generalGameOptions);
    }
}
