using System.Reflection;
using System.Text.Json;
using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Infrastructure.Generators;

public class WorldGenerator(IOpenAIGenerator openAIGenerator) : IWorldGenerator
{
    private readonly IOpenAIGenerator openAIGenerator = openAIGenerator;

        public async Task<List<string>> GenerateWorldNamesAsync(string worldDescription, int count)
    {
        var maxCharacterLength = 30;
        var systemPrompt = $"Generate {count} unique and captivating world names that reflect the essence and atmosphere of the described world. These names should be memorable, inspire curiosity or a sense of adventure, and each should be within {maxCharacterLength} characters. The output must be a bullet point list of names.";
        var userPrompt = $"Using the following description: '{worldDescription}', create evocative and unique names for a world. Ensure each name is within the character limit, and aim for a diverse range. The names should be presented in a bullet point list format. Example:\n- Name1\n- Name2\n- Name3";

        var result = await openAIGenerator.GenerateTextAsync(systemPrompt, userPrompt);
        
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
         await openAIGenerator.GenerateTextAsync(
            "You are a fictional writer.",
            $"Write a description for a RPG world based on the following world settings: {worldSettings}. " +
            "Don't mention world name in the description. " +
            "Don't write anything in bold." +
            "Don't add 'RPG' in the description. " +
            $"Remember, the maximum length for the world description should be {500} characters.");


    public async Task<Result<WorldSettings>> GenerateWorldSettingsAsync(List<string> worldThemes)
    {
        var geographySettings = await GenerateSettingsAsync<GeographySettings>(worldThemes);
        if (geographySettings.IsFailure) return Result.Failure<WorldSettings>(geographySettings.Error);

        var cultureSettings = await GenerateSettingsAsync<CultureSettings>(worldThemes);
        if (cultureSettings.IsFailure) return Result.Failure<WorldSettings>(cultureSettings.Error);

        var economySettings = await GenerateSettingsAsync<EconomySettings>(worldThemes);
        if (economySettings.IsFailure) return Result.Failure<WorldSettings>(economySettings.Error);

        var governanceSettings = await GenerateSettingsAsync<GovernanceSettings>(worldThemes);
        if (governanceSettings.IsFailure) return Result.Failure<WorldSettings>(governanceSettings.Error);

        var religionSettings = await GenerateSettingsAsync<ReligionSettings>(worldThemes);
        if (religionSettings.IsFailure) return Result.Failure<WorldSettings>(religionSettings.Error);

        var technologySettings = await GenerateSettingsAsync<TechnologySettings>(worldThemes);
        if (technologySettings.IsFailure) return Result.Failure<WorldSettings>(technologySettings.Error);

        var magicSettings = await GenerateSettingsAsync<MagicSettings>(worldThemes);
        if (magicSettings.IsFailure) return Result.Failure<WorldSettings>(magicSettings.Error);

        var worldSettings = new WorldSettings
        {
            Geography = geographySettings.Value,
            Culture = cultureSettings.Value,
            Economy = economySettings.Value,
            Government = governanceSettings.Value,
            Religion = religionSettings.Value,
            Technology = technologySettings.Value,
            Magic = magicSettings.Value
        };

        return Result.Success(worldSettings);
    }    

    public async Task<Result<T>> GenerateSettingsAsync<T>(List<string> themes)
    {
        var systemPrompt = ConstructSystemPrompt<T>();
        string userPrompt = $"Generate {typeof(T).Name} for a game world based on the world themes: {string.Join(", ", themes.Select(theme => theme.ToLower()))}. \n";

        var result = await openAIGenerator.GenerateJsonAsync(systemPrompt, userPrompt);

        if (result.IsSuccess)
        {
            var settings = JsonSerializer.Deserialize<T>(result.Value);
            return settings == null
                ? Result.Failure<T>($"Failed to deserialize {typeof(T).Name}: {result.Value} to JSON object.")
                : Result.Success(settings);
        }
        else
        {
            return Result.Failure<T>(result.Error);
        }
    }
    
    private string ConstructSystemPrompt<T>()
    {
        var properties = typeof(T).GetProperties();
        var schema = new Dictionary<string, object>();

        foreach (var property in properties)
        {
            var metadataAttribute = property.GetCustomAttribute<JsonPropertyMetadataAttribute>();
            if (metadataAttribute != null)
            {
                schema[property.Name] = new
                {
                    type = metadataAttribute.Type,
                    description = metadataAttribute.Description
                };
            }
        }

        var jsonSchema = JsonSerializer.Serialize(schema);

        return "You are a fictional world builder. " +
            $"Create {typeof(T).Name} based on the provided world themes. " +
            "I ask you for specific settings, and you will generate them in JSON object format. " +
            "Don't mention any world theme in the settings. \n" +
            "Use simple, clear, specific, and concise English language. \n\n" +
            $"Json Schema: \n{jsonSchema}";
    }
}
