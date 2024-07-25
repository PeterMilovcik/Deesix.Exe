using Deesix.Application.GameOptions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class WorldGenresGameOption(IRepository<World> worldRepository) : IGameOption
{
    private readonly IRepository<World> worldRepository = worldRepository
        ?? throw new ArgumentNullException(nameof(worldRepository));

    public string Title => "Choose a World Genre";

    public int Order => 1;

    public bool CanExecute(GameTurn gameTurn) => 
        gameTurn.Game.HasValue && 
        gameTurn.Game.Value.World is null &&
        gameTurn.LastOption is CreateNewGameOption;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var gameOptions = new List<IGameOption>
        {
            new SpecificWorldGenreGameOption("High Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Low Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Dystopian Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Magical Realism", worldRepository),
            new SpecificWorldGenreGameOption("Sword and Sorcery", worldRepository),
            new SpecificWorldGenreGameOption("Urban Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Paranormal Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Dark Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Superhero Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Steampunk Fantasy", worldRepository),
            new SpecificWorldGenreGameOption("Sci-fi Fantasy", worldRepository),
        
        };
        return Task.FromResult(gameTurn with 
        {
            Message = "Let's choose a specific world genre.",
            Question = "Which one would you like to choose?",
            GameOptions = gameOptions
        });
    }
}
