using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application.Interfaces;

public interface IWorldGenerator
{
    Task<Result<WorldSettings>> GenerateWorldSettingsAsync(List<string> worldThemes);
}