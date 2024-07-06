using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateWorldDescription(IGenerator generator, IUserInterface userInterface)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));
    private readonly IUserInterface userInterface = userInterface;

    public async Task<Result<string>> ExecuteAsync(WorldSettings worldSettings) => 
        await userInterface.ShowProgressAsync(
            "Generating world description...", 
            async () => await generator.World.GenerateWorldDescriptionAsync(worldSettings));
}
