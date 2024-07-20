using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public sealed class StartNewGameOption(IRepository<Game> gameRepository) : IGameOption
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Start a new game";

    public bool CanExecute(Maybe<Game> game) => game.HasNoValue;

    public Task<GameOptionResult> ExecuteAsync(Maybe<Game> game)
    {
        var createdGame = gameRepository.Add(new Game());        
        return Task.FromResult(new GameOptionResult(Title, Result.Success(createdGame)));
    }
}
