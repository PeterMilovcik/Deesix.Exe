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
    public void Title_Should_Return_Load_Games() => 
        loadGamesOption!.Title.Should().Be("Load Game", because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_2() => 
        loadGamesOption!.Order.Should().Be(2, because: "that is the expected order.");

    [Test]
    public void CanExecute_WhenGameHasValue_ReturnsFalse() => 
        loadGamesOption!.CanExecute(new GameTurn{ Game = new Game() })
            .Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void CanExecute_WhenGameHasNoValueAndRepositoryHasNoGames_ReturnsFalse()
    {
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game>());
        loadGamesOption!.CanExecute(new GameTurn())
            .Should().BeFalse(because: "there are no games in the repository.");
    }

    [Test]
    public void CanExecute_WhenGameHasNoValueAndRepositoryHasGames_ReturnsTrue()
    {
        // Arrange
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game> { new Game() });
        // Act & Assert
        bool result = loadGamesOption!.CanExecute(new GameTurn());
        result.Should().BeTrue(because: "there is a game in the repository.");
    }

    [Test]
    public void CanExecute_Should_Return_True_When_LastOption_Is_Null()
    {
        // Arrange
        var gameTurn = new GameTurn
        {
            LastOption = null!
        };
        // Act
        bool result = loadGamesOption!.CanExecute(gameTurn);
        // Assert
        result.Should().BeTrue();
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
