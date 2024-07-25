using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public class LoadGamesAction(IRepository<Game> gameRepository) : IGameAction
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Load Game";

    public int Order => 2;

    public bool CanExecute(GameTurn gameTurn) => 
        gameTurn.Game.HasNoValue && 
        gameRepository.GetAll().Any() && 
        gameTurn.LastGameAction is not LoadGamesAction;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var games = gameRepository.GetAll().ToList();
        var loadGameOptions = new List<IGameAction>();
        games.ForEach(game => loadGameOptions.Add(new LoadGameAction(game)));
        return Task.FromResult(gameTurn with 
        {
            Message = "Please choose a game to play.",
            Question = "Which one would you like to play?",
            GameActions = new List<IGameAction>(loadGameOptions)
        });
    }
}
