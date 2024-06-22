using Deesix.AI.Core;

namespace Deesix.AI.OpenAI;

public interface IBasicAIGenerator
{
    Task<Result<string>> GenerateAsync(string systemPrompt, string userPrompt);
}
