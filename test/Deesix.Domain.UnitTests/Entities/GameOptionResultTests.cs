using CSharpFunctionalExtensions;
using FluentAssertions;
using Deesix.Domain.Entities;

namespace Deesix.Domain.UnitTests;

[TestFixture]
public class GameOptionResultTests
{
    [Test]
    public void GameOptionResult_Ctor_WithNullMessage_ThrowsException()
    {
        // Arrange
        string message = null!;
        var game = Result.Success(new Game());
        
        // Act
        Action act = () => new GameOptionResult(message)
        {
            NextGameState = game
        };
        
        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'message')");
    }
    
    [Test]
    public void GameOptionResult_DefaultGame_IsFailedGameResult()
    {
        // Arrange
        string message = "Next game message.";
        
        // Act
        var gameOptionResult = new GameOptionResult(message);
        
        // Assert
        gameOptionResult.NextGameState.Should().Be(Result.Failure<Game>("No game loaded yet."));
    }
    
    [Test]
    public void GameOptionResult_Ctor_WithValidParameters_SetsProperties()
    {
        // Arrange
        string message = "Next game message.";
        var game = Result.Success(new Game());
        
        // Act
        var gameOptionResult = new GameOptionResult(message)
        {
            NextGameState = game
        };
        
        // Assert
        gameOptionResult.NextMessage.Should().Be(message);
        gameOptionResult.NextQuestion.Should().Be("What would you like to do next?");
        gameOptionResult.NextGameState.Should().Be(game);
        gameOptionResult.NextAdditionalGameOptions.Should().NotBeNull();
        gameOptionResult.NextAdditionalGameOptions.Should().BeEmpty();
        
    }
}
