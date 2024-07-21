using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public class LoadGameOption(Game game) : IGameOption
{
    private readonly Game gameToLoad = game;

    public string Title => 
        string.IsNullOrEmpty(gameToLoad.World?.Name)
            ? $"Load: {gameToLoad.GameId} - Unknown World"
            : $"Load: {gameToLoad.GameId} - {gameToLoad.World.Name}";

    public bool CanExecute(Maybe<Game> game) => game.HasNoValue;

    public Task<GameOptionResult> ExecuteAsync(Maybe<Game> game)
    {
        return Task.FromResult(new GameOptionResult("Game loaded successfully.")
        {
            NextGameState = Result.Success(gameToLoad)
        });
    }
}
