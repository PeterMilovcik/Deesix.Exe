using CSharpFunctionalExtensions;
using Deesix.Core;

namespace Deesix.AI;

public class RegionGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<string>> GenerateRegionDescriptionAsync(Realm realm) =>
        await openAIGenerator.GenerateAsync(            
            "Create a concise, detailed, and engaging description of a region within a specific realm.",            
            $"Describe a region within the '{realm.Name}' realm of the '{realm.World.Name}' world. " +
            "This description should delve into the region's geography, climate, flora, fauna, and cultural or historical significance within the realm. " +
            "Highlight what makes this region unique, such as its legendary landmarks, mythical creatures, or pivotal historical events. " +
            "Craft a narrative that is both rich and immersive, painting a vivid picture of the region and evoking its atmosphere. " +
            "Use sensory details to bring the region to life, creating a sense of wonder and making it an alluring destination for adventurers. " +
            "The description should be a blend of informative and captivating, merging factual content with imaginative storytelling to fully engage the reader. " +
            $"Ensure the final description is concise, aiming for a maximum of {300} characters.");

    public async Task<Result<string>> GenerateRegionNameAsync(string regionDescription) =>
        await openAIGenerator.GenerateAsync(
            "Generate a unique and evocative name for a region based on its description: \n",
            $"'{regionDescription}'\n\n" +
            "Craft a name that captures the essence and atmosphere of the region. " +
            "The name should be memorable, easy to pronounce, and encapsulate the region's unique characteristics, hinting at its mysteries, beauty, or danger. " +
            $"Ensure the name fits within a maximum of {30} characters, making it suitable for maps, guides, and narrative references. " +
            "Avoid common or generic names, aiming for something that sparks curiosity and invites exploration.");
}