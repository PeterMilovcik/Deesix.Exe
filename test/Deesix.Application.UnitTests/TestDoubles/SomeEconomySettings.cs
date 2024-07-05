using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests.TestDoubles;

internal sealed class SomeEconomySettings : EconomySettings
{
    public SomeEconomySettings()
    {
        Trade = "Some Trade";
        Currency = "Some Currency";
        Resources = "Some Resources";
        Labor = "Some Labor";
    }
}
