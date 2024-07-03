using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application.Interfaces;

public interface ILocationGenerator
{
    Task<Result<Location>> GenerateLocationAsync(Region region);
}