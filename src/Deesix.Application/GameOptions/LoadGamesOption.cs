using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public class LoadGamesOption(IRepository<Game> gameRepository) : IGameOption
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Load Game";

    public int Order => 2;

    public bool CanExecute(Maybe<Game> game)
    {
        if (game.HasValue) return false;
        return gameRepository.GetAll().Any();
    }

    public Task<GameOptionResult> ExecuteAsync(Maybe<Game> game)
    {
        var games = gameRepository.GetAll().ToList();
        var additionalGameOptions = new List<IGameOption>();
        games.ForEach(game => additionalGameOptions.Add(new LoadGameOption(game)));
        GameOptionResult result = new GameOptionResult("Please choose a game to play.");
        result.NextQuestion = "Which game would you like to play?";
        result.NextAdditionalGameOptions.AddRange(additionalGameOptions);
        return Task.FromResult(result);
    }
}
