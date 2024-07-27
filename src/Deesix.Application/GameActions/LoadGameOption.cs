using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public class LoadGameAction(Game game) : IGameAction
{
    private readonly Game gameToLoad = game 
        ?? throw new ArgumentNullException(nameof(game));

    public string Title => 
        string.IsNullOrEmpty(gameToLoad.World?.Name)
            ? $"Load: {gameToLoad.GameId} - Unknown World"
            : $"Load: {gameToLoad.GameId} - {gameToLoad.World.Name}";

    public int Order => 1;

    public bool CanExecute(GameTurn gameTurn) => gameTurn.Game.HasNoValue && gameTurn.LastGameAction is LoadGamesAction;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn) => Task.FromResult(gameTurn with 
    {
        Message = "Game loaded successfully.",
        Game = gameToLoad,
        GameActions = new List<IGameAction>()
    });
}
