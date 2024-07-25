using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public class LoadGameOption(Game game) : IGameOption
{
    private readonly Game gameToLoad = game 
        ?? throw new ArgumentNullException(nameof(game));

    public string Title => 
        string.IsNullOrEmpty(gameToLoad.World?.Name)
            ? $"Load: {gameToLoad.GameId} - Unknown World"
            : $"Load: {gameToLoad.GameId} - {gameToLoad.World.Name}";

    public int Order => 2;

    public bool CanExecute(GameTurn gameTurn) => gameTurn.Game.HasNoValue && gameTurn.LastOption is LoadGamesOption;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn) => Task.FromResult(gameTurn with 
    {
        Message = "Game loaded successfully.",
        Game = gameToLoad,
        GameOptions = new List<IGameOption>()
    });
}
