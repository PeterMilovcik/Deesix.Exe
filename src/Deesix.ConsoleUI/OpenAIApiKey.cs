using CSharpFunctionalExtensions;
using Deesix.Infrastructure;

namespace Deesix.ConsoleUI;

internal class OpenAIApiKey : IOpenAIApiKey
{
    public string? GetOpenAiApiKey()
    {
        return Environment.GetEnvironmentVariable("OPENAI_API_KEY");
    }

    public Result CheckOpenAiApiKey()
    {
        var openAiApiKey = GetOpenAiApiKey();
        return string.IsNullOrWhiteSpace(openAiApiKey) || !openAiApiKey.StartsWith("sk-")
            ? Result.Failure("OpenAI API key is invalid")
            : Result.Success();
    }
}
