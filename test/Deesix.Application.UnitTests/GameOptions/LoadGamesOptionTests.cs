using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests.GameOptions;

[TestFixture]
public class LoadGamesOptionTests : TestFixture
{
    private LoadGamesOption? loadGamesOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game>{new Game{GameId = 1}});
        var gameOptionFactory = Services.GetRequiredService<IGameOptionFactory>();
        loadGamesOption = gameOptionFactory.CreateGameOptions(new GameTurn()).OfType<LoadGamesOption>().FirstOrDefault();
        loadGamesOption.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public async Task ExecuteAsync_WhenGameHasNoValueAndRepositoryHasGames_ReturnsGameOptionResult()
    {
        // Arrange
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game> { new Game() });
        // Act
        var result = await loadGamesOption!.ExecuteAsync(new GameTurn());
        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be("Please choose a game to play.");
        result.Question.Should().Be("Which one would you like to play?");        
        result.GameOptions.Should().NotBeNull();
        result.GameOptions.Should().NotBeEmpty();
    }
}
