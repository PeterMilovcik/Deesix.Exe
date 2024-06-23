using CSharpFunctionalExtensions;
using Deesix.Core;

namespace Deesix.AI;

public class LocationGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<string>> GenerateLocationDescriptionAsync(Region region)
    {
        var systemPrompt = "Create a captivating and immersive location description.";
        var userPrompt = $"Envision a place within '{region.Name}', nestled in the '{region.Realm.Name}' realm of the '{region.Realm.World.Name}' world. " +
            $"This world abides by the following world settings: '{region.Realm.World.WorldSettings}'. " +
            "Craft a narrative that brings this location to life, highlighting its essence, ambiance, and the emotions it evokes. " +
            "Your description should be rich with sensory details and vivid imagery to transport the reader directly into this setting. " +
            $"Aim for a concise yet powerful depiction, with a maximum of {300} characters. " +
            "Please exclude any use of bold formatting and refrain from mentioning 'RPG'. " +
            "Let the focus be on painting a picture of this location's unique charm and allure. " +
            "Avoid including any additional information beyond the location description. " +
            "Remember, the goal is to create a compelling and evocative narrative. " +
            "Don't write anything else. ";
        var result = await openAIGenerator.GenerateAsync(systemPrompt, userPrompt);
        if (result.IsSuccess)
        {
            var description = result.Value;
            description = description.Replace("**", ""); // remove potential bold formatting
            return Result.Success(description);
        }
        else
        {
            return Result.Failure<string>(result.Error);
        }
    }

    public async Task<Result<string>> GenerateLocationNameAsync(string locationDescription) =>
        await openAIGenerator.GenerateAsync(            
            "Create a unique and evocative location name.",            
            $"Generate a name for a location described as '{locationDescription}'. " +
            "This name should encapsulate the essence and atmosphere of the place, hinting at its mysteries, beauty, or danger. " +
            "The name should be memorable, easy to pronounce, and fit the thematic elements of the world it belongs to. " +
            "Avoid common or generic names and aim for something that sparks curiosity and invites exploration.");
}
