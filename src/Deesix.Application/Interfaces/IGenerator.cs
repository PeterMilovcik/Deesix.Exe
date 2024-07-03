namespace Deesix.Application.Interfaces;

public interface IGenerator
{
    IWorldGenerator World { get; }
    IRealmGenerator Realm { get; }
}
