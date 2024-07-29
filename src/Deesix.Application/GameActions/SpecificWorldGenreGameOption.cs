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

    public bool CanExecute(Turn turn) => true;

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        var newWorld = new World
        {
            GameId = turn.Game.Value.Id,
            Genre = genre
        };
        turn.Game.Value.World = newWorld;
        return Task.FromResult(turn with
        {
            Message = $"World genre set to {genre}. Good choice!",
            Game = turn.Game.Value with
            {
                World = newWorld
            },
            Question = "What would you like to do next?",
            GameActions = []
        });
    }
}
