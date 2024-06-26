using CSharpFunctionalExtensions;
using OpenAI.Chat;

namespace Deesix.AI;

public class OpenAIGenerator : IBasicAIGenerator
{
    public OpenAIGenerator(string model, string apiKey)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
        ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }

    public string Model { get; }
    public string ApiKey { get; }

    public async Task<Result<string>> GenerateAsync(string systemPrompt, string userPrompt)
    {
        ChatClient client = new(Model, ApiKey);
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(systemPrompt),
            new UserChatMessage(userPrompt)
        };
        var chatCompletion = await client.CompleteChatAsync(messages);
        var result = chatCompletion.Value.ToString().Replace("```json", "").Replace("```", "").Trim();
        return Result.Success(result);
    }

    public async Task<Result<string>> GenerateJsonAsync(string systemPrompt, string userPrompt)
    {
        ChatClient client = new(Model, ApiKey);
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(systemPrompt),
            new UserChatMessage(userPrompt)
        };
        var chatCompletion = await client.CompleteChatAsync(messages, new ChatCompletionOptions { ResponseFormat = ChatResponseFormat.JsonObject });
        var result = chatCompletion.Value.ToString();
        return Result.Success(result);
    }
}
