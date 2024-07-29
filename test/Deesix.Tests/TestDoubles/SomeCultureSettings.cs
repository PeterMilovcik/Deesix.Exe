using Deesix.Domain.Entities;

namespace Deesix.Tests.TestDoubles;

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
