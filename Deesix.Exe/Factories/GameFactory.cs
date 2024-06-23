using Deesix.Core;
using Deesix.AI;
using CSharpFunctionalExtensions;

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

    public async Task<Result<Game>> CreateGameAsync()
    {
        Game? game = null;
        var world = await WorldFactory.CreateWorldAsync();
        if (world.IsSuccess)
        {
            var realm = await RealmFactory.CreateRealmAsync(world.Value!);
            if (realm.IsSuccess)
            {
                var region = await RegionFactory.CreateRegionAsync(realm.Value!);
                if (region.IsSuccess)
                {
                    var location = await LocationFactory.CreateLocationAsync(region.Value!);
                    if (location.IsSuccess)
                    {
                        game = new Game()
                        {
                            Id = world.Value!.Id,
                            World = world.Value!,
                            Character = new Character
                            {
                                Name = UI.PromptCharacterName(),
                                CurrentLocation = location.Value!
                            }
                        };
                    }
                    else
                    {
                        UI.ErrorMessage(location.Error);
                    }
                }
                else
                {
                    UI.ErrorMessage(region.Error);
                }                
            }
            else
            {
                UI.ErrorMessage(realm.Error);
            }
        }
        else
        {
            UI.ErrorMessage(world.Error);
        }
        return game is null 
            ? Result.Failure<Game>("Game not created.")
            : Result.Success(game);
    }
}