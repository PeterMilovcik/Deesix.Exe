using CSharpFunctionalExtensions;

namespace Deesix.Infrastructure.Generators;

public interface IOpenAIGenerator
{
    Task<Result<string>> GenerateTextAsync(string systemPrompt, string userPrompt);
    Task<Result<string>> GenerateJsonAsync(string systemPrompt, string userPrompt);
    Task<Result<T>> GenerateJsonObjectAsync<T>(string systemPrompt, string userPrompt) where T : class;
    Task<Result<List<string>>> GenerateMultipleTextAsync(
        string systemPrompt, string userPrompt, int numberOfOutputs, double temperature = 1.5, double topP = 1);
}