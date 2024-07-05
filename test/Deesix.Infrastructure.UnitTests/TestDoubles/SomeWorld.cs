using System.Diagnostics.CodeAnalysis;
using Deesix.Application.UnitTests.TestDoubles;
using Deesix.Domain.Entities;

namespace Deesix.Infrastructure.UnitTests;

public class SomeWorld : World
{
    [SetsRequiredMembers]
    public SomeWorld()
    {
        Name = "Some Name";
        Description = "Some Description";
        WorldSettings = new SomeWorldSettings();
    }
}
