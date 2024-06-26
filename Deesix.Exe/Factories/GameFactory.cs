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
        try
        {
            var world = await WorldFactory.CreateWorldAsync();
            if (world.IsFailure) return Result.Failure<Game>(world.Error);

            var realm = await RealmFactory.CreateRealmAsync(world.Value);
            if (realm.IsFailure) return Result.Failure<Game>(realm.Error);

            var region = await RegionFactory.CreateRegionAsync(realm.Value);
            if (region.IsFailure) return Result.Failure<Game>(realm.Error);

            var location = await LocationFactory.CreateLocationAsync(region.Value);
            if (location.IsFailure) return Result.Failure<Game>(location.Error);

            var game = new Game
            {
                Id = world.Value.Id,
                World = world.Value,
                Character = new Character
                {
                    Name = UI.PromptCharacterName(),
                    CurrentLocation = location.Value
                }
            };

            return Result.Success(game);
        }
        catch (Exception ex)
        {
            return Result.Failure<Game>($"An error occurred while creating the game: {ex.Message}");
        }
    }
}