using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public class WorldGenresGameOption(IRepository<World> worldRepository) : IGameAction
{
    private readonly IRepository<World> worldRepository = worldRepository
        ?? throw new ArgumentNullException(nameof(worldRepository));

    public string Title => "Choose a World Genre";

    public int Order => 1;

    public bool CanExecute(GameTurn gameTurn) => 
        gameTurn.Game.HasValue && 
        gameTurn.Game.Value.World is null &&
        gameTurn.LastGameAction is CreateNewGameAction;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var gameOptions = new List<IGameAction>
        {
            new SpecificWorldGenreGameAction("High Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Low Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Dystopian Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Magical Realism", worldRepository),
            new SpecificWorldGenreGameAction("Sword and Sorcery", worldRepository),
            new SpecificWorldGenreGameAction("Urban Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Paranormal Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Dark Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Superhero Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Steampunk Fantasy", worldRepository),
            new SpecificWorldGenreGameAction("Sci-fi Fantasy", worldRepository),
        
        };
        return Task.FromResult(gameTurn with 
        {
            Message = "Let's choose a specific world genre.",
            Question = "Which one would you like to choose?",
            GameActions = gameOptions
        });
    }
}
