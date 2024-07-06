using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application.Interfaces;

public interface IRealmGenerator
{
    Task<Result<GeneratedRealm>> GenerateRealmAsync(World world);
}