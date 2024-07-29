using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.GenerateWorldSettings;

public class ExecuteAsync : ActionTestFixture<GenerateWorldSettingsAction>
{
    [Test]
    public async Task Should_Return_Turn_With_Game_With_World_With_WorldSettings() => 
        (await Action!.ExecuteAsync(
            new Turn { Game = new Game { World = new World { Genre = "Test Genre" } } }))
                .Game.Value.World!.WorldSettings
                    .Should().NotBeNull(
                        because: "the turn has a game with a world that " + 
                            "has genre and no world settings");
}
