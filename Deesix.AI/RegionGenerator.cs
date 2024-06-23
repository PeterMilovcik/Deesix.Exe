using CSharpFunctionalExtensions;
using Deesix.Core;

namespace Deesix.AI;

public class RegionGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<string>> GenerateRegionDescriptionAsync(Realm realm) =>
        await openAIGenerator.GenerateAsync(
            $"You are a fictional writer tasked with creating a region within a realm.",
            $"Imagine a region within the realm of '{realm.Name}', which is part of the world '{realm.World.Name}' with settings '{realm.World.WorldSettings}'. " +
            $"The realm is described as '{realm.Description}'. " +
            $"Write a vivid and engaging description of this region, capturing its unique characteristics and atmosphere. " +
            $"Ensure the description is concise, limited to {300} characters. " +
            "Avoid using bold formatting or the term 'RPG' in the description. " +
            "Focus solely on the region's description.");

    public async Task<Result<string>> GenerateRegionNameAsync(string regionDescription) =>
        await openAIGenerator.GenerateAsync(
            $"You are a fictional writer.",
            $"Write a captivating name of RPG region described as '{regionDescription}'. " +
            $"Ensure the name is limited to {30} characters. " +
            "Don't write anything in bold. " +
            "Don't add 'RPG' in the name. " +
            "Don't write anything else.");
}