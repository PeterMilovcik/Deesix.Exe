using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Infrastructure.Generators;

public class LocationGenerator : ILocationGenerator
{
    public Task<Result<Location>> GenerateLocationAsync(Region region)
    {
        throw new NotImplementedException();
    }
}
