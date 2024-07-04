using CSharpFunctionalExtensions;
using OpenAI.Chat;

namespace Deesix.Infrastructure.Generators;

public class OpenAIGenerator(IOpenAIApiKey openAIApiKey) : IOpenAIGenerator
{
    private readonly IOpenAIApiKey openAIApiKey = openAIApiKey;
    private const string Model = "gpt-3.5-turbo"; // TODO: Move to configuration

    public async Task<Result<string>> GenerateJsonAsync(string systemPrompt, string userPrompt) => 
        await GenerateAsync(systemPrompt, userPrompt, ChatResponseFormat.JsonObject);

    public async Task<Result<string>> GenerateTextAsync(string systemPrompt, string userPrompt) => 
        await GenerateAsync(systemPrompt, userPrompt, ChatResponseFormat.Text);

    private async Task<Result<string>> GenerateAsync(string systemPrompt, string userPrompt, ChatResponseFormat chatResponseFormat)
    {
        var apiKey = openAIApiKey.GetOpenAiApiKey();
        if (string.IsNullOrEmpty(apiKey))
        {
            return Result.Failure<string>("OpenAI API key not found.");
        }
        ChatClient client = new(Model, apiKey);
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(systemPrompt),
            new UserChatMessage(userPrompt)
        };
        try
        {
            var chatCompletion = await client.CompleteChatAsync(messages, 
                new ChatCompletionOptions { ResponseFormat = chatResponseFormat });
            var result = chatCompletion.Value.ToString();
            return Result.Success(result);
        }
        catch (Exception exception)
        {
            return Result.Failure<string>("Error generating json: " + exception.Message);
        }
    }
}
