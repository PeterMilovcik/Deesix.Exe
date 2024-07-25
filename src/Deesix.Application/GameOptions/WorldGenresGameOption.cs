using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class WorldGenresGameOption : IGameOption
{
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
            new SpecificWorldGenreGameOption("High Fantasy"),
            new SpecificWorldGenreGameOption("Low Fantasy"),
            new SpecificWorldGenreGameOption("Dystopian Fantasy"),
            new SpecificWorldGenreGameOption("Magical Realism"),
            new SpecificWorldGenreGameOption("Sword and Sorcery"),
            new SpecificWorldGenreGameOption("Urban Fantasy"),
            new SpecificWorldGenreGameOption("Paranormal Fantasy"),
            new SpecificWorldGenreGameOption("Dark Fantasy"),
            new SpecificWorldGenreGameOption("Superhero Fantasy"),
            new SpecificWorldGenreGameOption("Steampunk Fantasy"),
            new SpecificWorldGenreGameOption("Sci-fi Fantasy")
        
        };
        return Task.FromResult(gameTurn with 
        {
            Message = "Let's choose a specific world genre.",
            Question = "Which one would you like to choose?",
            GameOptions = gameOptions
        });
    }
}
