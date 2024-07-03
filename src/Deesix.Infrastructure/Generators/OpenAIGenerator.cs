namespace Deesix.Infrastructure.Generators;

public class OpenAIGenerator(IOpenAIApiKey openAIApiKey) : IOpenAIGenerator
{
    private readonly IOpenAIApiKey openAIApiKey = openAIApiKey;
}
