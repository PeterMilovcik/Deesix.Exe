using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public class NewGame(
    IGenerator generator,
    IUserInterface userInterfacem,
    IRepository<Realm> realmRepository,
    IRepository<World> worldRepository)
{
    private readonly IGenerator generator = generator;
    private readonly IUserInterface userInterface = userInterfacem;
    private readonly IRepository<Realm> realmRepository = realmRepository;
    private readonly IRepository<World> worldRepository = worldRepository;

    public async Task<Result<Game>> ExecuteAsync()
    {
        var worldThemes = userInterface.PromptThemes();
        var worldSettings = await userInterface.ShowProgressAsync(
            "Generating world settings...", 
            async () => await generator.World.GenerateWorldSettingsAsync(worldThemes));
        worldSettings.MapError(e => Result.Failure<Game>($"Failed to generate world settings: {e}"));
        return await worldSettings.Map(ws => OnWorldSettingsGenerated(ws));
    }

    private async Task<Result<Game>> OnWorldSettingsGenerated(WorldSettings worldSettings)
    {
        var worldDescription = await userInterface.ShowProgressAsync(
            "Generating world description...", 
            async () => await generator.World.GenerateWorldDescriptionAsync(worldSettings));
        worldDescription.MapError(e => Result.Failure<Game>($"Failed to generate world description: {e}"));
        return await worldDescription.Map(wd => OnWorldDescriptionGenerated(wd, worldSettings));
    }

    private async Task<Result<Game>> OnWorldDescriptionGenerated(string worldDescription, WorldSettings worldSettings)
    {
        var worldNames = await userInterface.ShowProgressAsync(
            "Generating world names...", 
            async () => await generator.World.GenerateWorldNamesAsync(worldDescription, 10));
        var worldName = userInterface.SelectFromOptions("[green]Select a world name[/]", worldNames);
        var createdWorld = new World
        {
            Name = worldName,
            Description = worldDescription,
            WorldSettings = worldSettings,
        };
        
        var addedWorld = worldRepository.Add(createdWorld);

        var generatedRealm = await userInterface.ShowProgressAsync(
            "Generating realm...", 
            async () => await generator.Realm.GenerateRealmAsync(createdWorld));

        generatedRealm.MapError(e => Result.Failure<Game>($"Failed to generate realm: {e}"));
        return await generatedRealm.Map(r => OnGeneratedRealm(r, addedWorld));
    }

    private Task<Result<Game>> OnGeneratedRealm(GeneratedRealm realm, World world)
    {
        var createdRealm = new Realm
        {
            WorldId = world.Id,
            Name = realm.Name,
            Description = realm.Description,
        };

        var addedRealm = realmRepository.Add(createdRealm);

        return Task.FromResult(Result.Failure<Game>("Not implemented"));
    }
}
