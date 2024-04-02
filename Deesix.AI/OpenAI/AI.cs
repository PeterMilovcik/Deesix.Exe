using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using Deesix.Exe.Core;
using Deesix.Core;
using System.Text.Json;

namespace Deesix.AI.OpenAI;

public class AI
{
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

    public async Task<string> GenerateWorldDescriptionAsync(List<string> themes)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 300;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User, 
                $"Write a description for a RPG world based on the following theme(s): {string.Join(", ", themes)}. " +
                "Don't mention world name in the description. " +
                "Don't write anything in bold." +
                "Don't add 'RPG' in the description. " +
                $"Remember, the maximum length for the world description should be {maxCharacterLength} characters.")
        };
        return await GenerateAsync(api, messages);
    }

    public async Task<List<string>> GenerateWorldNamesAsync(string worldDescription, int count)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 30;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"Write a list of {count} suitable names for a RPG world based on the provided world description. \n" +
                $"Ensure each name is limited to {maxCharacterLength} characters. \n" +
                $"The list should be in the form of comma-separated items without any order numbers or bullet points. \n" + 
                " Example: \n" +
                "name1, name2, name3"),
            new Message(Role.User,
                $"Generate {count} names for a RPG world based on the following description: '{worldDescription}'. \n" +
                $"Remember, the maximum length for each world name should be {maxCharacterLength} characters. ")
        };
        var generatedText = await GenerateAsync(api, messages);
        return generatedText.Split(",").Select(name => name.Trim()).ToList();
    }

    public async Task<string> GenerateRealmDescriptionAsync(World world)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 300;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User,
                $"Write a vivid description of RPG realm in the world. " +
                $"Ensure the description is engaging yet concise, limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the description. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(api, messages);
    }

    public async Task<string> GenerateRealmNameAsync(World world, string realmDescription)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 30;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User,
                $"Write a captivating name of a RPG realm described as '{realmDescription}'. " +
                $"Ensure the name is limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the name. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(api, messages);
    }

    public async Task<string> GenerateRegionDescriptionAsync(World world, Realm realm)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 300;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User,
                $"Write a vivid description of RPG region. " +
                $"Ensure the description is engaging yet concise, limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the description. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(api, messages);
    }

    public async Task<string> GenerateRegionNameAsync(World world, Realm realm, string regionDescription)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 30;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User,
                $"Write a captivating name of RPG region described as '{regionDescription}'. " +
                $"Ensure the name is limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the name. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(api, messages);
    }

    public async Task<string> GenerateLocationDescriptionAsync(World world, Realm realm, Region region)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 300;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User,
                $"Write a vivid description of RPG location. " +
                $"Ensure the description is engaging yet concise, limited to {maxCharacterLength} characters. " +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the description. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(api, messages);
    }

    public async Task<string> GenerateLocationNameAsync(World world, Realm realm, Region region, string locationDescription)
    {
        using var api = new OpenAIClient(ApiKey);
        var maxCharacterLength = 30;
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional writer."),
            new Message(Role.User,
                $"Write a captivating name of RPG location described as '{locationDescription}'. " +
                $"Ensure the name is limited to {maxCharacterLength} characters." +
                "Don't write anything in bold. " +
                "Don't add 'RPG' in the name. " +
                "Don't write anything else."),
        };
        return await GenerateAsync(api, messages);
    }

    private static async Task<string> GenerateAsync(OpenAIClient api, List<Message> messages)
    {
        var chatRequest = new ChatRequest(messages, Model.GPT3_5_Turbo);
        var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
        var choice = response.FirstChoice;
        return choice.Message;
    }

    public async Task<WorldSettings?> GenerateWorldSettingsAsync(List<string> themes, string jsonWorldSettingsScheme)
    {
        using var api = new OpenAIClient(ApiKey);
        var messages = new List<Message>
        {
            new Message(Role.System,
                $"You are a fictional world builder."),
            new Message(Role.User,
                $"Create a world settings based on the provided world themes: {string.Join(", ", themes.Select(theme => theme.ToLower()))}. \n" +
                $"Ensure that the generated world settings are formatted as a JSON object based on the provided JSON scheme: \n " +
                "``` json\n " +
                $"{jsonWorldSettingsScheme}\n" +
                "```\n\n" +
                "Don't write anything in bold. " +
                "Don't write anything else. " +
                "Remember, I want you to generate JSON object, not a JSON schema. "),
        };
        var result = await GenerateAsync(api, messages);
        return JsonSerializer.Deserialize<WorldSettings>(result);
    }
}
