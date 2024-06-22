using Deesix.AI.Core;
using Deesix.Exe.Core;

namespace Deesix.AI.OpenAI;

public class LocationGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<string>> GenerateLocationDescriptionAsync(Region region) =>
        await openAIGenerator.GenerateAsync(            
            $"You are a fictional writer tasked with creating a location within a region.",            
            $"Imagine a location within the region of '{region.Name}', which is part of the realm '{region.Realm.Name}' in the world of '{region.Realm.World.Name}' with settings '{region.Realm.World.WorldSettings}'. " +
            $"Write a vivid and engaging description of this location, capturing its unique characteristics and atmosphere. " +
            $"Ensure the description is engaging yet concise, limited to {300} characters. " +
            "Avoid using bold formatting or the term 'RPG' in the description. " +
            "Focus solely on the location's description.");

    public async Task<Result<string>> GenerateLocationNameAsync(string locationDescription) =>
        await openAIGenerator.GenerateAsync(
            $"You are a fictional writer.",
            $"Write a captivating name of RPG location described as '{locationDescription}'. " +
            $"Ensure the name is limited to {30} characters." +
            "Don't write anything in bold. " +
            "Don't add 'RPG' in the name. " +
            "Don't write anything else.");
}
