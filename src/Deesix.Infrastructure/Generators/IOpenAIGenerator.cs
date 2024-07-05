using CSharpFunctionalExtensions;

namespace Deesix.Infrastructure.Generators;

public interface IOpenAIGenerator
{
    Task<Result<string>> GenerateTextAsync(string systemPrompt, string userPrompt);
    Task<Result<string>> GenerateJsonAsync(string systemPrompt, string userPrompt);
    Task<Result<T>> GenerateJsonObjectAsync<T>(string systemPrompt, string userPrompt) where T : class;
}