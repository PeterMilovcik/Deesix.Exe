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
    private CreateNewGameOption? startNewGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        startNewGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<CreateNewGameOption>().FirstOrDefault();
        startNewGameOption.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public void Title_Should_Return_Start_New_Game() => 
        startNewGameOption!.Title.Should().Be("Create New Game", because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        startNewGameOption!.Order.Should().Be(1, because: "that is the expected order.");

    [Test]
    public void CanExecute_Should_Return_True_When_Game_Has_No_Value() =>
        startNewGameOption!.CanExecute(new GameTurn()).Should().BeTrue();
    
    [Test]
    public void CanExecute_Should_Return_False_When_Game_Has_Value() =>
        startNewGameOption!.CanExecute(new GameTurn { Game = new Game()}).Should().BeFalse();

    [Test]
    public async Task ExecuteAsync_Should_Return_GameOptionResult_With_Game_When_Game_Has_No_Value()
    {
        // Arrange
        var noGame = Maybe<Game>.None;
        var createdGame = new Game();
        GameRepositoryMock.Setup(x => x.Add(It.IsAny<Game>())).Returns(createdGame);
        // Act
        var nextGameTurn = await startNewGameOption!.ExecuteAsync(new GameTurn());
        // Assert
        nextGameTurn.Game.HasValue.Should().BeTrue();
        nextGameTurn.Message.Should().Be("Game created successfully! Get ready for an exciting adventure!");
    }
}
