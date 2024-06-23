using System.Text.Json;
using CSharpFunctionalExtensions;
using Deesix.Core;

namespace Deesix.AI;

public class WorldGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<List<string>> GenerateWorldNamesAsync(string worldDescription, int count)
    {
        var maxCharacterLength = 30;
        var systemPrompt = $"Generate {count} unique and captivating world names that reflect the essence and atmosphere of the described world. These names should be memorable, inspire curiosity or a sense of adventure, and each should be within {maxCharacterLength} characters. The output must be a bullet point list of names.";
        var userPrompt = $"Using the following description: '{worldDescription}', create evocative and unique names for a world. Ensure each name is within the character limit, and aim for a diverse range. The names should be presented in a bullet point list format. Example:\n- Name1\n- Name2\n- Name3";

        var result = await openAIGenerator.GenerateAsync(systemPrompt, userPrompt);
        
        List<string> names = new List<string>();
        if (result.IsSuccess)
        {
            names.AddRange(result.Value!
                                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(name => name.TrimStart('-').Trim())
                                .ToList());
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
