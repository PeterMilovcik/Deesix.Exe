using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public class LoadGameOption(IRepository<Game> gameRepository) : IGameOption
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Load a game";

    public bool CanExecute(Maybe<Game> game)
    {
        if (game.HasValue) return false;
        return gameRepository.GetAll().Any();
    }

    public Task<GameOptionResult> ExecuteAsync(Maybe<Game> game)
    {
        // TODO: add a list property with additional game options into the game option result class and fill it with loaded specific game options with games
        // use game Id for title and if the game has world, use also a world name.
        // implement this load game option using the TDD approach
        var games = gameRepository.GetAll().ToList();
        return Task.FromResult(new GameOptionResult(
            "Game started successfully! Get ready for an exciting adventure!"));
    }
}
