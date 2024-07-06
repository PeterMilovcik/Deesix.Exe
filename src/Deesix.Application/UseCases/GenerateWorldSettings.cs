using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateWorldSettings(IGenerator generator, IUserInterface userInterface)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));
    
    public async Task<Result<WorldSettings>> ExecuteAsync(List<string> worldThemes) => 
        await userInterface.ShowProgressAsync(
            "Generating world settings...", 
            async () => await generator.World.GenerateWorldSettingsAsync(worldThemes));
}
