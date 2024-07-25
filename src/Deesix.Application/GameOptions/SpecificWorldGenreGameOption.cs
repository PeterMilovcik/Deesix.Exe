using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class SpecificWorldGenreGameOption(string genre, IRepository<World> worldRepository) : IGameOption
{
    private readonly IRepository<World> worldRepository = worldRepository 
        ?? throw new ArgumentNullException(nameof(worldRepository));

    public string Title => genre;

    public int Order => 1;

    public bool CanExecute(GameTurn gameTurn) => true;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var world = new World
        {
            Genre = genre
        };
        var addedWorld = worldRepository.Add(world);
        return Task.FromResult(gameTurn with
        {
            Message = $"World genre set to {genre}. Good choice!",
            Game = gameTurn.Game.Value with
            {
                World = addedWorld
            },
            Question = "What would you like to do next?",
            GameOptions = []
        });
    }
}
