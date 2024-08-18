using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Actions;

public sealed class CreateNewAction(IRepository<Game> gameRepository) : IAction
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Create new game";
    public string ProgressTitle => "Creating new game...";

    public int Order => 1;

    public bool CanExecute(Turn turn) => 
        turn.Game.HasNoValue && 
        turn.LastAction is null;

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        var newGame = new Game();
        gameRepository.Add(newGame);
        return Task.FromResult(turn with
        {
            Message = "Game created successfully! Get ready for an exciting adventure!",
            Game = newGame,
            Actions = []
        });
    }
}
