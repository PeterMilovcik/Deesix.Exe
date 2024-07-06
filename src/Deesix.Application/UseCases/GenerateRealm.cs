using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Utilities;

namespace Deesix.Application;

public sealed class GenerateRealm(IGenerator generator, IUserInterface userInterface)
{
    private readonly IGenerator generator = generator;
    private readonly IUserInterface userInterface = userInterface;

    public async Task<Result<GeneratedRealm>> ExecuteAsync(World world) => 
        await userInterface.ShowProgressAsync(
            "Generating realm...", 
            async () => await generator.Realm.GenerateRealmAsync(world));

    public class GeneratedRealm
    {
        [JsonPropertyMetadata("string", "Short memorable name of the realm. Max. 30 characters.")]
        public required string Name { get; init; }
        [JsonPropertyMetadata("string", "Concise description of the realm. Max. 300 characters.")]
        public required string Description { get; init; }
    }
}
