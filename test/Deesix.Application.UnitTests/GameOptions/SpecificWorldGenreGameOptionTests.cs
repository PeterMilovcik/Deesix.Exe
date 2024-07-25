using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Deesix.Application.UnitTests.GameOptions;

public class SpecificWorldGenreGameOptionTests : TestFixture
{
    private const string Genre = "Test Genre";
    private Mock<IRepository<World>> worldRepositoryMock;
    private SpecificWorldGenreGameOption? specificWorldGenreGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        worldRepositoryMock = new Mock<IRepository<World>>();
        specificWorldGenreGameOption = new SpecificWorldGenreGameOption(Genre, worldRepositoryMock.Object);
    }

    [Test]
    public void Title_Should_Return_Genre() => 
        specificWorldGenreGameOption!.Title.Should().Be(Genre, 
            because: "that is the expected title.");
    
    [Test]
    public void Order_Should_Return_1() => 
        specificWorldGenreGameOption!.Order.Should().Be(1, 
            because: "that is the expected order.");
    
    [Test]
    public void CanExecute_Should_Return_True() => 
        specificWorldGenreGameOption!.CanExecute(new GameTurn()).Should().BeTrue();
    
    [Test]
    public async Task ExecuteAsync_Should_Return_GameTurn_With_New_World()
    {
        // Arrange
        var gameTurn = new GameTurn
        {
            Game = new Game()
        };
        var addedWorld = new World 
        {
            WorldId = 1, 
            Genre = Genre 
        };
        worldRepositoryMock.Setup(x => x.Add(It.IsAny<World>())).Returns(addedWorld);
        // Act
        var result = await specificWorldGenreGameOption!.ExecuteAsync(gameTurn);
        // Assert
        result.Game.Value.World.Should().NotBeNull();
        result.Game.Value.World!.Genre.Should().Be(Genre);
        result.Message.Should().Be($"World genre set to {Genre}. Good choice!");
        result.Question.Should().Be("What would you like to do next?");
        result.GameOptions.Should().BeEmpty();
    }
}
