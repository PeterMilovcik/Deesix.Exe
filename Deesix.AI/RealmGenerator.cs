using CSharpFunctionalExtensions;
using Deesix.Core;

namespace Deesix.AI;

public class RealmGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<string>> GenerateRealmDescriptionAsync(World world) => 
        await openAIGenerator.GenerateAsync(
            $"You are a fictional writer tasked with creating a realm within a world.",
            $"Imagine a realm within the world of '{world.Name}', which is described as '{world.Description}' " +
            $"and has the following settings: '{world.WorldSettings}'. " +
            $"Write a vivid and engaging description of this realm, capturing its unique characteristics and atmosphere. " +
            $"Ensure the description is concise, limited to {300} characters. " +
            "Avoid using bold formatting or the term 'RPG' in the description. " +
            "Focus solely on the realm's description.");

    public async Task<Result<string>> GenerateRealmNameAsync(string realmDescription) =>
        await openAIGenerator.GenerateAsync(
            $"You are a fictional writer.",
            $"Write a captivating name of a RPG realm described as '{realmDescription}'. " +
            $"Ensure the name is limited to {30} characters. " +
            "Don't write anything in bold. " +
            "Don't add 'RPG' in the name. " +
            "Don't write anything else.");
}