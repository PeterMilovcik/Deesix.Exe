using System.Reflection;
using System.Text.Json;
using CSharpFunctionalExtensions;
using Deesix.AI.Entities;
using Deesix.Core;
using Deesix.Core.Settings;
using Location = Deesix.AI.Entities.Location;

namespace Deesix.AI;

public class LocationGenerator(OpenAIGenerator openAIGenerator)
{
    private readonly OpenAIGenerator openAIGenerator = openAIGenerator ?? throw new ArgumentNullException(nameof(openAIGenerator));

    public async Task<Result<Location>> GenerateLocationAsync(Region region)
    {
        var userPrompt = $"Describe a location within '{region.Name}', part of the '{region.Realm.Name}' realm in the '{region.Realm.World.Name}' world. " +
            $"Consider the world settings: '{region.Realm.World.WorldSettings}'. " +
            "Provide a straightforward description that captures the key characteristics of this place, focusing on what one might see, hear, and smell. " +
            "Aim for clarity and simplicity in your depiction. " +
            "Avoid using bold formatting or mentioning 'RPG'. " +
            "Concentrate on the location's main features and atmosphere. " +
            "Keep the description direct and to the point, without additional unrelated details. " +
            "The goal is to give a clear and concise picture of the location. ";

        var systemPrompt = ConstructSystemPrompt<Location>();
        var result = await openAIGenerator.GenerateJsonAsync(systemPrompt, userPrompt);

        if (result.IsSuccess)
        {
            var settings = JsonSerializer.Deserialize<Location>(result.Value);
            return settings == null
                ? Result.Failure<Location>($"Failed to deserialize Location: {result.Value} to JSON object.")
                : Result.Success(settings);
        }
        else
        {
            return Result.Failure<Location>(result.Error);
        }
    }

    private string ConstructSystemPrompt<T>()
    {
        var properties = typeof(T).GetProperties();
        var schema = new Dictionary<string, object>();

        foreach (var property in properties)
        {
            var metadataAttribute = property.GetCustomAttribute<JsonPropertyMetadataAttribute>();
            if (metadataAttribute != null)
            {
                schema[property.Name] = new
                {
                    type = metadataAttribute.Type,
                    description = metadataAttribute.Description
                };
            }
        }

        var jsonSchema = JsonSerializer.Serialize(schema);

        return 
            "You are a fictional world builder. " +
            "Create a captivating and immersive location description." +
            $"Create location within the provided world region. " +
            "You will generate location in JSON object format. " +
            "Use simple, clear, specific, and descriptive English language. \n\n" +
            $"Json Schema: \n{jsonSchema}";
    }
}
