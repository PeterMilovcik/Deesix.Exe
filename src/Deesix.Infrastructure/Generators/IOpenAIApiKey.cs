using CSharpFunctionalExtensions;

namespace Deesix.Infrastructure;

public interface IOpenAIApiKey
{
    Result CheckOpenAiApiKey();
    string? GetOpenAiApiKey();
}
