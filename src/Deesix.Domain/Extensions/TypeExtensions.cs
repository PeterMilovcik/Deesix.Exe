using System.Reflection;
using System.Text.Json;
using Deesix.Domain.Utilities;

namespace Deesix.Domain.Extensions;

public static class TypeExtensions
{
    public static string GetJsonPropertyMetadataSchema(this Type type)
    {
        var properties = type.GetProperties();
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

        return JsonSerializer.Serialize(schema);
    }
}