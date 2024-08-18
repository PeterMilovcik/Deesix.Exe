using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Actions;

public class LoadGameAction(Game game) : IAction
{
    private readonly Game gameToLoad = game 
        ?? throw new ArgumentNullException(nameof(game));

    public string Title => 
        string.IsNullOrEmpty(gameToLoad.World?.Name)
            ? $"Load: {gameToLoad.GameId} - Unknown World"
            : $"Load: {gameToLoad.GameId} - {gameToLoad.World.Name}";
    
    public string ProgressTitle => "Loading game...";

    public int Order => 1;

    public bool CanExecute(Turn turn) => turn.Game.HasNoValue && turn.LastAction is LoadGamesAction;

    public Task<Turn> ExecuteAsync(Turn turn) => Task.FromResult(turn with 
    {
        Message = "Game loaded successfully.",
        Game = gameToLoad,
        Actions = new List<IAction>()
    });
}
