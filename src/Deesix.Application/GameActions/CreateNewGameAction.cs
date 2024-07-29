using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public sealed class CreateNewGameAction(IRepository<Game> gameRepository) : IGameAction
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Create New Game";

    public int Order => 1;

    public bool CanExecute(Turn turn) => 
        turn.Game.HasNoValue && 
        turn.LastGameAction is null;

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        var createdGame = gameRepository.Add(new Game());
        return Task.FromResult(turn with
        {
            Message = "Game created successfully! Get ready for an exciting adventure!",
            Game = createdGame,
            GameActions = []
        });
    }
}
