using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Actions;

public class LoadGamesAction(IRepository<Game> gameRepository) : IAction
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Load game";

    public string ProgressTitle => "Loading games...";

    public int Order => 2;

    public bool CanExecute(Turn turn) => 
        turn.Game.HasNoValue && 
        gameRepository.GetAll().Any() && 
        turn.LastAction is not LoadGamesAction;

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        var games = gameRepository.GetAll().ToList();
        var loadGameOptions = new List<IAction>();
        games.ForEach(game => loadGameOptions.Add(new LoadGameAction(game)));
        return Task.FromResult(turn with 
        {
            Message = "Please choose a game to play.",
            Question = "Which one would you like to play?",
            Actions = new List<IAction>(loadGameOptions)
        });
    }
}
