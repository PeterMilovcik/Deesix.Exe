using CSharpFunctionalExtensions;
using Deesix.Core;
using Deesix.Domain.Entities;

namespace Deesix.AI;

public class RealmGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<string>> GenerateRealmDescriptionAsync(World world)
    {
        var systemPrompt = $"You are a fictional writer tasked with creating a vivid and immersive realm within a world.";
        
        var userPrompt = $"Envision a realm within a world described as '{world.Description}' " + 
            $"and shaped by these settings: '{world.WorldSettings}'. " +
            $"Craft a concise, yet engaging description of this realm, emphasizing its unique features and atmosphere. " +
            $"The description should be limited to {300} characters. " + 
            "Avoid mentioning the realm name and refrain from using bold markdown. " + 
            "Focus entirely on bringing the realm to life.";                    

        return await openAIGenerator.GenerateAsync(systemPrompt, userPrompt);
    }

    public async Task<Result<string>> GenerateRealmNameAsync(string realmDescription) =>
        await openAIGenerator.GenerateAsync(
            $"You are a fictional writer.",
            $"Write a captivating name of a RPG realm described as '{realmDescription}'. " +
            $"Ensure the name is limited to {30} characters. " +
            "Don't write anything in bold. " +
            "Don't add 'RPG' in the name. " +
            "Don't write anything else.");
}