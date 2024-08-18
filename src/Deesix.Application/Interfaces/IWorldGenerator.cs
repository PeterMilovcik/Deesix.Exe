using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application.Interfaces;

public interface IWorldGenerator
{
    Task<Result<WorldSettings>> GenerateWorldSettingsAsync(string genre);
    Task<Result<string>> GenerateWorldDescriptionAsync(WorldSettings worldSettings);
    Task<List<string>> GenerateWorldNamesAsync(string worldDescription, int count);
    Task<Result<List<string>>> GenerateWorldDescriptionsAsync(World world, int count);
}