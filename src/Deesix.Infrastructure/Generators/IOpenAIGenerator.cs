using CSharpFunctionalExtensions;

namespace Deesix.Infrastructure.Generators;

public interface IOpenAIGenerator
{
    Task<Result<string>> GenerateTextAsync(string systemPrompt, string userPrompt);
    Task<Result<string>> GenerateJsonAsync(string systemPrompt, string userPrompt);
}