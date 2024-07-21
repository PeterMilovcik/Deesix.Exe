using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Deesix.Application.GameOptions;
using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests;

[TestFixture]
public class LoadGamesOptionTests : TestFixture
{
    private LoadGamesOption? loadGamesOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        loadGamesOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<LoadGamesOption>().FirstOrDefault();
        loadGamesOption.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public void CanExecute_WhenGameHasValue_ReturnsFalse() => 
        loadGamesOption!.CanExecute(Maybe<Game>.From(new Game())).Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void CanExecute_WhenGameHasNoValueAndRepositoryHasNoGames_ReturnsFalse() => 
        loadGamesOption!.CanExecute(Maybe<Game>.None).Should().BeFalse(because: "there are no games in the repository.");

    [Test]
    public void CanExecute_WhenGameHasNoValueAndRepositoryHasGames_ReturnsTrue()
    {
        // Arrange
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game> { new Game() });
        // Act & Assert
        bool result = loadGamesOption!.CanExecute(Maybe<Game>.None);
        result.Should().BeTrue(because: "there is a game in the repository.");
    }

    [Test]
    public async Task ExecuteAsync_WhenGameHasNoValueAndRepositoryHasGames_ReturnsGameOptionResult()
    {
        // Arrange
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game> { new Game() });
        // Act
        GameOptionResult result = await loadGamesOption!.ExecuteAsync(Maybe<Game>.None);
        // Assert
        result.Should().NotBeNull();
        result.NextMessage.Should().Be("Please choose a game to play.");
        result.NextQuestion.Should().Be("Which game would you like to play?");        
        result.NextAdditionalGameOptions.Should().NotBeNull();
        result.NextAdditionalGameOptions.Should().NotBeEmpty();
    }
}
