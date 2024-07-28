using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests;

public class GameMasterTestFixture : TestFixture
{
    protected IGameMaster GameMaster { get; private set; }

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        GameMaster = Services.GetRequiredService<IGameMaster>();
        GameMaster.Should().NotBeNull(because: "the game master should be registered in the services");
    }
}
