using System.Text.Json;
using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Extensions;

namespace Deesix.Infrastructure.Generators;

public class RealmGenerator(IOpenAIGenerator openAIGenerator) : IRealmGenerator
{
    private readonly IOpenAIGenerator openAIGenerator = openAIGenerator;

    public async Task<Result<GeneratedRealm>> GenerateRealmAsync(World world)
    {
        var jsonSchema = typeof(GeneratedRealm).GetJsonPropertyMetadataSchema();

        var systemPrompt = $"You are a fictional writer tasked with creating a vivid and immersive realm within a world." + 
            "I ask you for specific realm within a world, and you will generate it in JSON object format. " +
            "Don't mention the world in description, focus on describing the realm. \n" +
            "Use simple, clear, specific, and concise English language. \n\n" +
            $"Json Schema: \n{jsonSchema} \n\n" +
            "Example: \n" +
            $"{JsonSerializer.Serialize(GeneratedRealm.Example)}";
        var userPrompt = $"Envision a realm within a world described as '{world.Description}' " + 
            $"and shaped by these settings: '{world.WorldSettings}'. " +
            "Don't repeat realm name in the realm description. " +
            "Focus entirely on bringing the realm to life.";

        return await openAIGenerator.GenerateJsonObjectAsync<GeneratedRealm>(systemPrompt, userPrompt);
    }
}
