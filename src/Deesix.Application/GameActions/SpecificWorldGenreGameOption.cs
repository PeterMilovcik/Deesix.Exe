using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public class SpecificWorldGenreGameAction(string genre, IRepository<World> worldRepository) : IGameAction
{
    private readonly IRepository<World> worldRepository = worldRepository 
        ?? throw new ArgumentNullException(nameof(worldRepository));

    public string Title => genre;

    public int Order => 1;

    public bool CanExecute(GameTurn gameTurn) => true;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var newWorld = new World
        {
            GameId = gameTurn.Game.Value.Id,
            Genre = genre
        };
        gameTurn.Game.Value.World = newWorld;
        return Task.FromResult(gameTurn with
        {
            Message = $"World genre set to {genre}. Good choice!",
            Game = gameTurn.Game.Value with
            {
                World = newWorld
            },
            Question = "What would you like to do next?",
            GameActions = []
        });
    }
}
