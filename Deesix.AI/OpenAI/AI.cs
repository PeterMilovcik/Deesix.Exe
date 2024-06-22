namespace Deesix.AI.OpenAI;

public class Generators
{
    public Generators(OpenAIGenerator openAIGenerator)
    {
        World = new WorldGenerator(openAIGenerator);
        Realm = new RealmGenerator(openAIGenerator);
        Region = new RegionGenerator(openAIGenerator);
        Location = new LocationGenerator(openAIGenerator);
    }

    public WorldGenerator World { get; }
    public RealmGenerator Realm { get; }
    public RegionGenerator Region { get; }
    public LocationGenerator Location { get; }
}
