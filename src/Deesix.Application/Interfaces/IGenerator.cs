namespace Deesix.Application.Interfaces;

public interface IGenerator
{
    IWorldGenerator World { get; }
    IRealmGenerator Realm { get; }
    IRegionGenerator Region { get; }
    ILocationGenerator Location { get; }
}
