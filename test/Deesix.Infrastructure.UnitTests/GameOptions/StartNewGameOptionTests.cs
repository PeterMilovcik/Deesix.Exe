using CSharpFunctionalExtensions;
using Deesix.Application.GameOptions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Infrastructure.UnitTests.GameOptions;

[TestFixture]
public class StartNewGameOptionTests : TestFixture
{
    private StartNewGameOption? startNewGameOption;
    private IRepository<Game> gameRepository;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        startNewGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<StartNewGameOption>().FirstOrDefault();
        startNewGameOption.Should().NotBeNull(because: "it is registered as a service.");
        gameRepository = Services.GetRequiredService<IRepository<Game>>();
        gameRepository.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public async Task StartNewGameOption_Should_CreateNewGame()
    {
        // Arrange
        var noGame = Maybe<Game>.None;

        // Act
        var gameOptionsResult = await startNewGameOption!.ExecuteAsync(noGame);

        // Assert
        gameOptionsResult.NextGameState.IsSuccess.Should().BeTrue();
        var game = gameRepository.GetById(gameOptionsResult.NextGameState.Value.Id);
        game.Should().NotBeNull();
        game.Should().Be(gameOptionsResult.NextGameState.Value);
    }
}
