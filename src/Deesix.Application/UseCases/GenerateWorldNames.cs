using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;

namespace Deesix.Application;

public sealed class GenerateWorldNames(IGenerator generator, IUserInterface userInterface)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));
    private readonly IUserInterface userInterface = userInterface;

    public async Task<Result<List<string>>> ExecuteAsync(string worldDescription, int count) => 
        await userInterface.ShowProgressAsync(
            "Generating world names...", 
            async () => await generator.World.GenerateWorldNamesAsync(worldDescription, count));
}