using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Infrastructure.Generators;

public class CharacterGenerator(IOpenAIGenerator openAIGenerator) : ICharacterGenerator
{
    private readonly IOpenAIGenerator openAIGenerator = openAIGenerator;

    public async Task<List<string>> GenerateCharacterNamesAsync(World world, int count)
    {
        var maxCharacterLength = 30;
        var systemPrompt = $"Generate {count} unique and captivating character names that are suitable for " + 
            $"the essence and atmosphere of the described world. These names should be within {maxCharacterLength} characters. " + 
            $"The output must be a bullet point list of names.";
        var userPrompt = $"Here is the world description: '{world.Description}', genre: '{world.Genre}', be creative and " + 
            "generate unique character names. Ensure each name is within the character limit, and aim for a diverse range. " + 
            "The names should be presented in a bullet point list format. Example:\n- Name1\n- Name2\n- Name3";

        var result = await openAIGenerator.GenerateTextAsync(systemPrompt, userPrompt);
        
        List<string> names = new List<string>();
        if (result.IsSuccess)
        {
            names.AddRange(result.Value!
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(name => name.TrimStart('-').Trim())
                .ToList());
        }
        return names;
    }
}
