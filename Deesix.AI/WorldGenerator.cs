using System.Text.Json;
using CSharpFunctionalExtensions;
using Deesix.Core;
using Deesix.Core.Settings;

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

    public async Task<Result<WorldSettings>> GenerateWorldSettingsAsync(List<string> themes)
    {
        var systemPrompt = "You are a fictional world builder. " + 
            $"Create a world settings based on the provided world themes: {string.Join(", ", themes.Select(theme => theme.ToLower()))}. \n" +
            "I'll ask you for specific world settings detail and you will generate max. 100 characters description. " +
            "Don't mention themes in the description. " + 
            "Use simple, clear and concise language. ";

        var worldSettings = new WorldSettings
        {
            Geography = new GeographySettings(),
            Culture = new CultureSettings(),
            Economy = new EconomySettings(),
            Government = new GovernanceSettings(),
            Religion = new ReligionSettings(),
            Technology = new TechnologySettings(),
            Magic = new MagicSettings()
        };
        var result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Geography Settings - Landmasses");
        if (result.IsSuccess) worldSettings.Geography.Landmasses = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Geography Settings - Landmarks");
        if (result.IsSuccess) worldSettings.Geography.Landmarks = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Geography Settings - Biomes");
        if (result.IsSuccess) worldSettings.Geography.Biomes = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Geography Settings - Climate");
        if (result.IsSuccess) worldSettings.Geography.Climate = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Geography Settings - Resources");
        if (result.IsSuccess) worldSettings.Geography.Resources = result.Value;

        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Culture Settings - Languages");
        if (result.IsSuccess) worldSettings.Culture.Languages = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Culture Settings - Societies");
        if (result.IsSuccess) worldSettings.Culture.Societies = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Culture Settings - Traditions");
        if (result.IsSuccess) worldSettings.Culture.Traditions = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Culture Settings - Beliefs");
        if (result.IsSuccess) worldSettings.Culture.Beliefs = result.Value;

        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Economy Settings - Trade");
        if (result.IsSuccess) worldSettings.Economy.Trade = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Economy Settings - Currency");
        if (result.IsSuccess) worldSettings.Economy.Currency = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Economy Settings - Resources");
        if (result.IsSuccess) worldSettings.Economy.Resources = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Economy Settings - Labor");
        if (result.IsSuccess) worldSettings.Economy.Labor = result.Value;

        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Government Settings - Type");
        if (result.IsSuccess) worldSettings.Government.Type = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Government Settings - Law");
        if (result.IsSuccess) worldSettings.Government.Law = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Government Settings - Military");
        if (result.IsSuccess) worldSettings.Government.Military = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Government Settings - Diplomacy");
        if (result.IsSuccess) worldSettings.Government.Diplomacy = result.Value;

        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Religion Settings - Pantheons");
        if (result.IsSuccess) worldSettings.Religion.Pantheons = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Religion Settings - Cults");
        if (result.IsSuccess) worldSettings.Religion.Cults = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Religion Settings - Rituals");
        if (result.IsSuccess) worldSettings.Religion.Rituals = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Religion Settings - Temples");
        if (result.IsSuccess) worldSettings.Religion.Temples = result.Value;

        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Technology Settings - Tools");
        if (result.IsSuccess) worldSettings.Technology.Tools = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Technology Settings - Weapons");
        if (result.IsSuccess) worldSettings.Technology.Weapons = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Technology Settings - Armors");
        if (result.IsSuccess) worldSettings.Technology.Armors = result.Value;

        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Magic Settings - Schools");
        if (result.IsSuccess) worldSettings.Magic.Schools = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Magic Settings - Spells");
        if (result.IsSuccess) worldSettings.Magic.Spells = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Magic Settings - Artifacts");
        if (result.IsSuccess) worldSettings.Magic.Artifacts = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Magic Settings - Creatures");
        if (result.IsSuccess) worldSettings.Magic.Creatures = result.Value;
        result = await openAIGenerator.GenerateAsync(systemPrompt, $"Generate Magic Settings - Intensity");
        if (result.IsSuccess) worldSettings.Magic.Intensity = result.Value;

        return Result.Success(worldSettings);
    }
}
