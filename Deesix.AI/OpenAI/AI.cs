using Deesix.Exe.Core;
using Deesix.Core;
using System.Text.Json;
using OpenAI.Chat;

namespace Deesix.AI.OpenAI;

public class AI
{
    public const string Model4o = "gpt-4o";
    public const string Model35Turbo = "gpt-3.5-turbo";
    public AI(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException($"'{nameof(apiKey)}' cannot be null or whitespace.", nameof(apiKey));
        }
        if (!apiKey.StartsWith("sk-"))
        {
            throw new ArgumentException($"'{nameof(apiKey)}' must start with 'sk-'", nameof(apiKey));
        }

        ApiKey = apiKey;
    }

    private string ApiKey { get; }

    public async Task<string?> GenerateWorldDescriptionAsync(List<string> themes)
    {
        var maxCharacterLength = 300;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a description for a RPG world based on the following theme(s): {string.Join(", ", themes)}. " +
                "Don't mention world name in the description. " +
                "Don't write anything in bold." +
                "Don't add 'RPG' in the description. " +
                $"Remember, the maximum length for the world description should be {maxCharacterLength} characters.")
        };
        return await GenerateAsync(messages);
    }

    public async Task<List<string>> GenerateWorldNamesAsync(string worldDescription, int count)
    {
        var maxCharacterLength = 30;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"Write a list of {count} suitable names for a RPG world based on the provided world description. \n" +
                $"Ensure each name is limited to {maxCharacterLength} characters. \n" +
                $"The list should be in the form of comma-separated items without any order numbers or bullet points. \n" + 
                " Example: \n" +
                "name1, name2, name3"),
            new UserChatMessage(
                $"Generate {count} names for a RPG world based on the following description: '{worldDescription}'. \n" +
                $"Remember, the maximum length for each world name should be {maxCharacterLength} characters. ")
        };
        var generatedText = await GenerateAsync(messages) ?? string.Empty;
        return generatedText.Split(",").Select(name => name.Trim()).ToList();
    }

    public async Task<string?> GenerateRealmDescriptionAsync(World world)
    {
        var maxCharacterLength = 300;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a vivid description of RPG realm in the world. " +
                $"Ensure the description is engaging yet concise, limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the description. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(messages);
    }

    public async Task<string?> GenerateRealmNameAsync(World world, string realmDescription)
    {
        var maxCharacterLength = 30;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a captivating name of a RPG realm described as '{realmDescription}'. " +
                $"Ensure the name is limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the name. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(messages);
    }

    public async Task<string?> GenerateRegionDescriptionAsync(World world, Realm realm)
    {
        var maxCharacterLength = 300;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a vivid description of RPG region. " +
                $"Ensure the description is engaging yet concise, limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the description. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(messages);
    }

    public async Task<string?> GenerateRegionNameAsync(World world, Realm realm, string regionDescription)
    {
        var maxCharacterLength = 30;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a captivating name of RPG region described as '{regionDescription}'. " +
                $"Ensure the name is limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the name. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(messages);
    }

    public async Task<string?> GenerateLocationDescriptionAsync(World world, Realm realm, Region region)
    {
        var maxCharacterLength = 300;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a vivid description of RPG location. " +
                $"Ensure the description is engaging yet concise, limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the description. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(messages);
    }

    public async Task<string?> GenerateLocationNameAsync(World world, Realm realm, Region region, string locationDescription)
    {
        var maxCharacterLength = 30;
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional writer."),
            new UserChatMessage(
                $"Write a captivating name of RPG location described as '{locationDescription}'. " +
                $"Ensure the name is limited to {maxCharacterLength} characters." +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the name. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(messages);
    }

    private async Task<string?> GenerateAsync(List<ChatMessage> messages)
    {
        ChatClient client = new(Model35Turbo, ApiKey);
        var chatCompletion = await client.CompleteChatAsync(messages);
        return chatCompletion.Value.ToString().Replace("```json", "").Replace("```", "").Trim();        
    }

    public async Task<WorldSettings?> GenerateWorldSettingsAsync(List<string> themes, string jsonWorldSettingsScheme)
    {
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"You are a fictional world builder."),
            new UserChatMessage(
                $"Create a world settings based on the provided world themes: {string.Join(", ", themes.Select(theme => theme.ToLower()))}. \n" +
                $"Ensure that the generated world settings are formatted as a JSON object based on the provided JSON scheme: \n " +
                "``` json\n " +
                $"{jsonWorldSettingsScheme}\n" +
                "```\n\n" +
                "Don't write anything in bold. " +
                "Don't write anything else. " +
                "Remember, I want you to generate JSON object, not a JSON schema. "),
        };
        var result = await GenerateAsync(messages);
        if (string.IsNullOrEmpty(result)) return null;
        return JsonSerializer.Deserialize<WorldSettings>(result);
    }
}
