using CSharpFunctionalExtensions;
using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Deesix.Application.UnitTests.GameOptions;

public class CreateNewGameOptionTests : TestFixture
{
    private CreateNewGameOption? createNewGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        createNewGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<CreateNewGameOption>().FirstOrDefault();
        createNewGameOption.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public void Title_Should_Return_Start_New_Game() => 
        createNewGameOption!.Title.Should().Be("Create New Game", because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        createNewGameOption!.Order.Should().Be(1, because: "that is the expected order.");

    [Test]
    public void CanExecute_Should_Return_True_When_Game_Has_No_Value() =>
        createNewGameOption!.CanExecute(new GameTurn()).Should().BeTrue();
    
    [Test]
    public void CanExecute_Should_Return_False_When_Game_Has_Value() =>
        createNewGameOption!.CanExecute(new GameTurn { Game = new Game()}).Should().BeFalse();

    [Test]
    public void CanExecute_Should_Return_False_When_LastOption_Is_LoadGamesOption()
    {
        // Arrange
        var gameTurn = new GameTurn
        {
            LastOption = new LoadGamesOption(GameRepositoryMock.Object)
        };
        // Act
        bool result = createNewGameOption!.CanExecute(gameTurn);
        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task ExecuteAsync_Should_Return_GameTurn_With_Empty_GameOptions()
    {
        // Arrange
        var createdGame = new Game();
        GameRepositoryMock.Setup(x => x.Add(It.IsAny<Game>())).Returns(createdGame);
        var gameTurn = new GameTurn
        {
            GameOptions = new List<IGameOption>
            {
                new Mock<IGameOption>().Object,
            }
        };
        // Act
        var nextGameTurn = await createNewGameOption!.ExecuteAsync(gameTurn);
        // Assert
        nextGameTurn.GameOptions.Should().BeEmpty();
    }

    [Test]
    public async Task ExecuteAsync_Should_Return_GameTurn_With_Game_When_Game_Has_No_Value()
    {
        // Arrange
        var createdGame = new Game();
        GameRepositoryMock.Setup(x => x.Add(It.IsAny<Game>())).Returns(createdGame);
        // Act
        var nextGameTurn = await createNewGameOption!.ExecuteAsync(new GameTurn());
        // Assert
        nextGameTurn.Game.HasValue.Should().BeTrue();
        nextGameTurn.Message.Should().Be("Game created successfully! Get ready for an exciting adventure!");
    }
}
