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

    public bool CanExecute(GameTurn gameTurn) => 
        gameTurn.Game.HasNoValue && 
        gameTurn.LastGameAction is null;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var createdGame = gameRepository.Add(new Game());
        return Task.FromResult(gameTurn with
        {
            Message = "Game created successfully! Get ready for an exciting adventure!",
            Game = createdGame,
            GameActions = []
        });
    }
}