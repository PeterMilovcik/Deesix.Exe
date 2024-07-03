using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Infrastructure.Generators;

public class WorldGenerator : IWorldGenerator
{
    public Task<Result<World>> GenerateWorldAsync(WorldSettings worldSettings)
    {
        throw new NotImplementedException();
    }

    public Task<Result<WorldSettings>> GenerateWorldSettingsAsync(List<string> worldThemes)
    {
        throw new NotImplementedException();
    }
}
