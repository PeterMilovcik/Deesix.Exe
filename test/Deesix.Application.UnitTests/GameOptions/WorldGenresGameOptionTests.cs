using CSharpFunctionalExtensions;
using Deesix.Application.GameActions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Deesix.Application.UnitTests.GameOptions;

public class WorldGenresGameOptionTests : TestFixture
{
    private WorldGenresGameOption? worldGenresGameOption;
    private GameTurn validGameTurnInput;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game>{new Game{GameId = 1}});
        var gameOptionFactory = Services.GetRequiredService<IGameOptionFactory>();
        worldGenresGameOption = gameOptionFactory.CreateGameOptions(new GameTurn()).OfType<WorldGenresGameOption>().FirstOrDefault();
        worldGenresGameOption.Should().NotBeNull(because: "it is registered as a service.");
        
        validGameTurnInput = new GameTurn{ Game = new Game() };
    }

    [Test]
    public void Title_Should_Return_Choose_World_Genre() => 
        worldGenresGameOption!.Title.Should().Be("Choose a World Genre", because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        worldGenresGameOption!.Order.Should().Be(1, because: "that is the expected order.");

    [Test]
    public void CanExecute_Should_Return_False_When_Game_HasNoValue() => 
        worldGenresGameOption!.CanExecute(new GameTurn{ Game = Maybe.None }).Should().BeFalse(because: "the game has no value.");
    
    [Test]
    public void CanExecute_Should_Return_False_When_Game_HasValue_And_World_IsNotNull() => 
        worldGenresGameOption!.CanExecute(new GameTurn{ Game = new Game{ World = new World() } }).Should().BeFalse(because: "the game has a value and the world is not null.");
    
    [Test]
    public void CanExecute_Should_Return_True_When_Game_HasValue_And_World_IsNull_And_LastOption_Is_CreateNewGameOption()
    {
        // Arrange
        var gameRepository = new Mock<IRepository<Game>>().Object;
        var lastOption = new CreateNewGameOption(gameRepository);
        // Act and Assert
        worldGenresGameOption!.CanExecute(validGameTurnInput with 
        {        
            LastOption = lastOption
        }).Should().BeTrue(
            because: "the game has a value, the world is null, " + 
            "and the last option is CreateNewGameOption.");
    }

    [Test]
    public void CanExecute_Should_Return_False_When_Game_HasValue_And_World_IsNull_And_LastOption_Is_Not_CreateNewGameOption() => 
        worldGenresGameOption!.CanExecute(validGameTurnInput).Should().BeFalse(
            because: "the game has a value, the world is null, " +
            "and the last option is not CreateNewGameOption.");

    [Test]
    public async Task ExecuteAsync_Should_Return_New_GameTurn_With_Message_To_Choose_A_Specific_World_Genre()
    {
        var newGameTurn = await worldGenresGameOption!.ExecuteAsync(validGameTurnInput);
        newGameTurn.Message.Should().Be("Let's choose a specific world genre.", because: "that is the expected message.");
    }

    [Test]
    public async Task ExecuteAsync_Should_Return_New_GameTurn_With_Question_Which_World_Genre_Would_You_Like_To_Choose()
    {
        var newGameTurn = await worldGenresGameOption!.ExecuteAsync(validGameTurnInput);
        newGameTurn.Question.Should().Be("Which one would you like to choose?", because: "that is the expected question.");
    }

    [Test]
    public async Task ExecuteAsync_Should_Return_New_GameTurn_With_Specific_GameOptions()
    {
        var newGameTurn = await worldGenresGameOption!.ExecuteAsync(validGameTurnInput);
        newGameTurn.GameOptions.Should().NotBeNullOrEmpty(because: "it should have specific game options.");
    }

    [TestCase("High Fantasy")]
    [TestCase("Low Fantasy")]
    [TestCase("Dystopian Fantasy")]
    [TestCase("Magical Realism")]
    [TestCase("Sword and Sorcery")]
    [TestCase("Urban Fantasy")]
    [TestCase("Paranormal Fantasy")]
    [TestCase("Dark Fantasy")]
    [TestCase("Superhero Fantasy")]
    [TestCase("Steampunk Fantasy")]
    [TestCase("Sci-fi Fantasy")]    
    public async Task ExecuteAsync_Should_Return_New_GameTurn_With_SpecificWorldGenreGameOption_With_Title(string genreName)
    {
        var newGameTurn = await worldGenresGameOption!.ExecuteAsync(validGameTurnInput);
        var specificWorldGenreGameOption = newGameTurn.GameOptions.OfType<SpecificWorldGenreGameAction>().FirstOrDefault(x => x.Title == genreName);
        specificWorldGenreGameOption.Should().NotBeNull(because: $"it should have a specific world genre game option with title {genreName}.");
    }
}
