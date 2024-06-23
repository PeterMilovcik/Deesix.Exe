using System.Text.Json;
using Deesix.Core;
using FluentResults;

namespace Deesix.AI;

public class WorldGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<List<string>> GenerateWorldNamesAsync(string worldDescription, int count)
    {
        var maxCharacterLength = 30;
        var result = await openAIGenerator.GenerateAsync($"Write a list of {count} suitable names for a RPG world based on the provided world description. \n" +
                $"Ensure each name is limited to {maxCharacterLength} characters. \n" +
                $"The list should be in the form of comma-separated items without any order numbers or bullet points. \n" + 
                " Example: \n" +
                "name1, name2, name3", 
                $"Generate {count} names for a RPG world based on the following description: '{worldDescription}'. \n" +
                $"Remember, the maximum length for each world name should be {maxCharacterLength} characters. ");
        List<string> names = new List<string>();
        if (result.IsSuccess)
        {
            names.AddRange(result.Value!.Split(",").Select(name => name.Trim()).ToList());
        }
        return names;
    }

    public async Task<Result<string>> GenerateWorldDescriptionAsync(WorldSettings worldSettings) =>
         await openAIGenerator.GenerateAsync(
            "You are a fictional writer.",
            $"Write a description for a RPG world based on the following world settings: {worldSettings}. " +
            "Don't mention world name in the description. " +
            "Don't write anything in bold." +
            "Don't add 'RPG' in the description. " +
            $"Remember, the maximum length for the world description should be {300} characters.");

    public async Task<WorldSettings?> GenerateWorldSettingsAsync(List<string> themes, string jsonWorldSettingsScheme)
    {
        var result = await openAIGenerator.GenerateAsync(
            $"You are a fictional world builder.",
            $"Create a world settings based on the provided world themes: {string.Join(", ", themes.Select(theme => theme.ToLower()))}. \n" +
            $"Ensure that the generated world settings are formatted as a JSON object based on the provided JSON scheme: \n " +
            "``` json\n " +
            $"{jsonWorldSettingsScheme}\n" +
            "```\n\n" +
            "Don't write anything in bold. " +
            "Don't write anything else. " +
            "Remember, I want you to generate JSON object, not a JSON schema. ");
        return result.IsSuccess
            ? JsonSerializer.Deserialize<WorldSettings>(result.Value!) is { } worldSettings
                ? worldSettings
                : null
            : null;
    }
}
