using Deesix.Core.Settings;

namespace Deesix.AI.Entities;

public sealed class Location
{
    [JsonPropertyMetadata("string", "Location name, up to 30 characters.")]
    public required string Name { get; set; }

    [JsonPropertyMetadata("string", "Type of terrain in the location, such as forest, desert, mountain, etc.")]
    public required string Terrain { get; set; }
    
    [JsonPropertyMetadata("string", "Climate of the location, such as tropical, arid, temperate, etc.")]
    public required string Climate { get; set; }

    [JsonPropertyMetadata("string", "Visual description of the location, up to 300 characters. Should describe what a character can see when looking at the location, including landscape, objects, and any notable visual features.")]
    public required string VisualDescription { get; set; }

    [JsonPropertyMetadata("string", "Sound description of the location, up to 200 characters. Should detail the ambient sounds a character might hear in this location, such as wildlife, weather, or human activity.")]
    public required string SoundDescription { get; set; }

    [JsonPropertyMetadata("string", "Smell description of the location, up to 100 characters. Should convey the scents a character might detect in this location, including natural, artificial, pleasant, or unpleasant odors.")]
    public required string SmellDescription { get; set; }

    public override string ToString() => 
        "**Location**:\n" +
        $"- Name: {Name}\n" + 
        $"- Visual Description: {VisualDescription}\n" + 
        $"- Sound Description: {SoundDescription}\n" + 
        $"- Smell Description: {SmellDescription}";
}