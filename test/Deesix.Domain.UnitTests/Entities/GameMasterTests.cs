using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Domain.UnitTests.Entities;

[TestFixture]
public class GameMasterTests : TestFixture
{
    private IGameMaster gameMaster;

    public override void SetUp()
    {
        base.SetUp();
        gameMaster = Services.GetRequiredService<IGameMaster>();
        gameMaster.Should().NotBeNull(because: "it is registered as a service.");
    }
}
