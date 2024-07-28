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
            new SpecificWorldGenreGameAction("High Fantasy - Epic tales set in entirely fictional worlds with grand themes, mythical creatures, and magical elements, often involving a battle between good and evil.", worldRepository),
            new SpecificWorldGenreGameAction("Low Fantasy - Stories set in the real world but with magical elements that are hidden or rare, focusing on everyday characters encountering the extraordinary.", worldRepository),
            new SpecificWorldGenreGameAction("Dystopian Fantasy - Dark, oppressive worlds often featuring a society in decay, strict control, or post-apocalyptic settings where survival is key.", worldRepository),
            new SpecificWorldGenreGameAction("Magical Realism - Realistic settings blended seamlessly with magical elements, used to explore complex themes and human experiences.", worldRepository),
            new SpecificWorldGenreGameAction("Sword and Sorcery - Action-packed adventures with heroic characters, often featuring intense battles, magic, and a clear distinction between good and evil.", worldRepository),
            new SpecificWorldGenreGameAction("Urban Fantasy - Fantastical elements interwoven with modern city life, featuring supernatural beings like vampires, werewolves, or wizards living among humans.", worldRepository),
            new SpecificWorldGenreGameAction("Paranormal Fantasy - Focuses on supernatural phenomena such as ghosts, vampires, and otherworldly events, often blending horror and fantasy elements.", worldRepository),
            new SpecificWorldGenreGameAction("Dark Fantasy - Combines fantasy with horror elements, creating a brooding, ominous atmosphere with complex characters and morally grey areas.", worldRepository),
            new SpecificWorldGenreGameAction("Superhero Fantasy - Characters with extraordinary powers fighting against evil, set in both real and fictional worlds with a focus on heroism and justice.", worldRepository),
            new SpecificWorldGenreGameAction("Steampunk Fantasy - Features advanced steam-powered technology within a historical or fantastical setting, blending Victorian aesthetics with imaginative inventions.", worldRepository),
            new SpecificWorldGenreGameAction("Sci-fi Fantasy - Merges science fiction elements with traditional fantasy, involving futuristic technology, space travel, and magical elements.", worldRepository),
        };
        return Task.FromResult(gameTurn with 
        {
            Message = "Let's choose a specific world genre.",
            Question = "Which one would you like to choose?",
            GameActions = gameOptions
        });
    }

}
