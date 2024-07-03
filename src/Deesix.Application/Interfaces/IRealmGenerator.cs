using Deesix.Domain.Entities;

namespace Deesix.Application.Interfaces;

public interface IRealmGenerator
{
    Task<Realm> GenerateRealmAsync(World world);
}