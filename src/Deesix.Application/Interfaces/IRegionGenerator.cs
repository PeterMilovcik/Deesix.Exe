using Deesix.Domain.Entities;

namespace Deesix.Application.Interfaces;

public interface IRegionGenerator
{
    Task<Region> GenerateRegionAsync(Realm realm);
}