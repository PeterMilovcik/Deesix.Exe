using FluentResults;

namespace Deesix.AI;

public interface IBasicAIGenerator
{
    Task<Result<string>> GenerateAsync(string systemPrompt, string userPrompt);
}
