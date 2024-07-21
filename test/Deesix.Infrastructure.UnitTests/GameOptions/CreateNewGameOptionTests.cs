using Deesix.Application.GameOptions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Infrastructure.UnitTests.GameOptions;

[TestFixture]
public class CreateNewGameOptionTests : TestFixture
{
    private CreateNewGameOption? createNewGameOption;
    private IRepository<Game> gameRepository;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        createNewGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<CreateNewGameOption>().FirstOrDefault();
        createNewGameOption.Should().NotBeNull(because: "it is registered as a service.");
        gameRepository = Services.GetRequiredService<IRepository<Game>>();
        gameRepository.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public async Task ExecuteAsync_Should_CreateNewGame()
    {
        // Arrange
        var gameTurn = new GameTurn();

        // Act
        var nextGameTurn = await createNewGameOption!.ExecuteAsync(gameTurn);

        // Assert
        nextGameTurn.Game.HasValue.Should().BeTrue();
        var game = gameRepository.GetById(nextGameTurn.Game.Value.Id);
        game.Should().NotBeNull();
        game.Should().Be(nextGameTurn.Game.Value);
    }
}
