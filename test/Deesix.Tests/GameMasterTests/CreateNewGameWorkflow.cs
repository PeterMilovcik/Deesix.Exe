using FluentAssertions;

namespace Deesix.Tests.GameMasterTests;

public class CreateNewGameWorkflow : GameMasterTestFixture
{
    [Test]
    public async Task Should_Be_Successful()
    {
        await CreateNewGame();
        await ShowWorldGendres();
        await ChooseWorldGenre();
        await GenerateWorldSettings();
        GameMaster.Turn.Game.Value.World.Should().NotBeNull(because: "the world should be created");
    }
}
