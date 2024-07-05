namespace Deesix.Application.Interfaces;

public interface IRealmGenerator
{
    Task<GenerateRealm.Response> GenerateRealmAsync(GenerateRealm.Request request);
}