namespace Deesix.Application.Interfaces;

public interface IGenerator
{
    ICharacterGenerator Character { get; }
    IWorldGenerator World { get; }
    IRealmGenerator Realm { get; }
    IRegionGenerator Region { get; }
    ILocationGenerator Location { get; }
}
