using CSharpFunctionalExtensions;
using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Extensions;

namespace Deesix.Infrastructure.Generators;

public class RealmGenerator(IOpenAIGenerator openAIGenerator) : IRealmGenerator
{
    private readonly IOpenAIGenerator openAIGenerator = openAIGenerator;

    public async Task<Result<GenerateRealm.GeneratedRealm>> GenerateRealmAsync(World world)
    {
        var jsonSchema = typeof(GenerateRealm.GeneratedRealm).GetJsonPropertyMetadataSchema();

        var systemPrompt = $"You are a fictional writer tasked with creating a vivid and immersive realm within a world." + 
            "I ask you for specific realm within a world, and you will generate it in JSON object format. " +
            "Don't mention the world in description, focus on describing the realm. \n" +
            "Use simple, clear, specific, and concise English language. \n\n" +
            $"Json Schema: \n{jsonSchema}";
        var userPrompt = $"Envision a realm within a world described as '{world.Description}' " + 
            $"and shaped by these settings: '{world.WorldSettings}'. " +
            $"Craft a concise, yet engaging description of this realm, emphasizing its unique features and atmosphere. " +
            $"The description should be limited to {300} characters. " + 
            "Avoid mentioning the realm name and refrain from using bold markdown. " + 
            "Focus entirely on bringing the realm to life.";
        
        return await openAIGenerator.GenerateJsonObjectAsync<GenerateRealm.GeneratedRealm>(systemPrompt, userPrompt);
    }
}
