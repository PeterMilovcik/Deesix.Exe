using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests.TestDoubles;

internal sealed class SomeCultureSettings : CultureSettings
{
    public SomeCultureSettings()
    {
        Languages = "Some Languages";
        Societies = "Some Societies";
        Traditions = "Some Traditions";
        Beliefs = "Some Beliefs";
    }
}
