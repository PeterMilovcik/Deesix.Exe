namespace Deesix.Domain.Entities;

[AttributeUsage(AttributeTargets.Property)]
public class JsonPropertyMetadataAttribute : Attribute
{
    public string Type { get; set; }
    public string Description { get; set; }
    
    public JsonPropertyMetadataAttribute(string type, string description)
    {
        Type = type;
        Description = description;
    }
}
