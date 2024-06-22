using Deesix.AI.OpenAI;
using Deesix.Exe.Core;

namespace Deesix.Exe.Factories;

public class GameFactory
{
    public GameFactory(Generators ai, UserInterface ui)
    {
        UI = ui ?? throw new ArgumentNullException(nameof(ui));
        WorldFactory = new WorldFactory(UI, ai);
        RealmFactory = new RealmFactory(UI, ai);
        RegionFactory = new RegionFactory(UI, ai);
        LocationFactory = new LocationFactory(UI, ai);
    }

    private UserInterface UI { get; }
    private WorldFactory WorldFactory { get; }
    private RealmFactory RealmFactory { get; }
    private RegionFactory RegionFactory { get; }
    private LocationFactory LocationFactory { get; }

    public async Task<Game> CreateGameAsync()
    {
        var world = await WorldFactory.CreateWorldAsync();
        var realm = await RealmFactory.CreateRealmAsync(world);
        var region = await RegionFactory.CreateRegionAsync(realm);
        var location = await LocationFactory.CreateLocationAsync(region);

        return new Game()
        {
            Id = world.Id,
            World = world,
            Character = new Character
            {
                Name = UI.PromptCharacterName(),
                CurrentLocation = location
            }
        };
    }
}