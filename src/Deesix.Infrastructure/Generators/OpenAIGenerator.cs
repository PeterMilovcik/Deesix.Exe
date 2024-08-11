using System.ClientModel;
using System.Globalization;
using System.Text.Json;
using CSharpFunctionalExtensions;
using OpenAI.Chat;

namespace Deesix.Infrastructure.Generators;

public class OpenAIGenerator(IOpenAIApiKey openAIApiKey) : IOpenAIGenerator
{
    private readonly IOpenAIApiKey openAIApiKey = openAIApiKey;
    private const string Model = "gpt-4o-mini"; // TODO: Move to configuration

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

    public async Task<Result<T>> GenerateJsonObjectAsync<T>(string systemPrompt, string userPrompt) where T : class
    {
        var result = await GenerateJsonAsync(systemPrompt, userPrompt);
        if (result.IsSuccess)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<T>(result.Value);
                return obj == null
                    ? Result.Failure<T>($"Failed to deserialize {typeof(T).Name}: {result.Value} to JSON object. Deserialized object is null.")
                    : Result.Success(obj);
            }
            catch (JsonException exception)
            {
                return Result.Failure<T>($"Failed to deserialize  {typeof(T).Name}: {result.Value} to JSON object. Error: {exception.Message}");
            }
        }
        else
        {
            return Result.Failure<T>(result.Error);
        }
    }

    public async Task<Result<List<string>>> GenerateMultipleTextAsync(string systemPrompt, string userPrompt, int numberOfOutputs, double temperature = 1.5, double topP = 1)
    {
        if (temperature > 1.75)
        {
            throw new ArgumentOutOfRangeException(nameof(temperature), "Using a temperature higher than 1.75 may not generate reasonable output.");
        }

        if (temperature <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(temperature), "Temperature must be higher than 0.");
        }

        if (topP > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(topP), "Using a topP higher than 1 may not generate reasonable output.");
        }

        if (topP < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(topP), "TopP must be higher than 0.");
        }

        var apiKey = openAIApiKey.GetOpenAiApiKey();
        if (string.IsNullOrEmpty(apiKey))
        {
            return Result.Failure<List<string>>("OpenAI API key not found.");
        }
        ChatClient client = new(Model, apiKey);
        try
        {
            string inputJson = $@"
            {{
                ""model"": ""{Model}"",
                ""messages"": [
                    {{
                        ""role"": ""system"",
                        ""content"": ""{systemPrompt}""
                    }},
                    {{
                        ""role"": ""user"",
                        ""content"": ""{userPrompt}""
                    }}
                    ],
                ""n"": {numberOfOutputs},
                ""temperature"": {temperature.ToString(CultureInfo.InvariantCulture)},
                ""top_p"": {topP.ToString(CultureInfo.InvariantCulture)}
            }}";
            BinaryData binaryData = BinaryData.FromString(inputJson);
            using BinaryContent binaryContent = BinaryContent.Create(binaryData);
            var clientResult = await client.CompleteChatAsync(binaryContent);
            BinaryData output = clientResult.GetRawResponse().Content;
            using JsonDocument outputAsJson = JsonDocument.Parse(output.ToString());
            List<string> result = new List<string>();
            foreach (var choice in outputAsJson.RootElement.GetProperty("choices").EnumerateArray())
            {
                string text = choice.GetProperty("message")!.GetProperty("content")!.GetString()!;
                result.Add(text);
            }
            return Result.Success(result);
        }
        catch (Exception exception)
        {
            return Result.Failure<List<string>>("Error: " + exception.Message);
        }
    }
}
