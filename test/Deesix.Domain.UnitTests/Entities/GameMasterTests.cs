using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Deesix.Domain.UnitTests.Entities;

[TestFixture]
public class GameMasterTests : TestFixture
{
    private IGameMaster gameMaster;

    public override void SetUp()
    {
        base.SetUp();
        gameMaster = Services.GetRequiredService<IGameMaster>();
        gameMaster.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public void GameHasNoValue_ByDefault() => gameMaster.Game.HasNoValue.Should().BeTrue();

    [Test]
    public void GetMessage_DefaultValue() => gameMaster.GetMessage().Should().Be(
        "Welcome, adventurer! I am here to guide you through this game. " + 
        "As the game master, I have prepared a thrilling adventure filled with " + 
        "challenges and mysteries. ");

    [Test]
    public void GetQuestion_DefaultValue() => gameMaster.GetQuestion().Should().Be(
        "Are you ready to embark on this epic journey?");

    [Test]
    public async Task ProcessOptionAsync_WhenOptionReturnsNextAdditionalGameOptions_UsesThemInNextCallToGetOptions()
    {
        // Arrange
        var gameOptionToExecuteMock = new Mock<IGameOption>();
        var gameOptionResult = new GameOptionResult("Next message");
        gameOptionToExecuteMock.Setup(x => x.CanExecute(It.IsAny<Maybe<Game>>())).Returns(true);
        gameOptionToExecuteMock.Setup(x => x.ExecuteAsync(It.IsAny<Maybe<Game>>())).ReturnsAsync(gameOptionResult);
        var nextGameOptionMock = new Mock<IGameOption>();
        nextGameOptionMock.Setup(x => x.CanExecute(It.IsAny<Maybe<Game>>())).Returns(true);
        var nextGameOption = nextGameOptionMock.Object;
        gameOptionResult.NextAdditionalGameOptions.Add(nextGameOption);
        // Act
        await gameMaster.ProcessOptionAsync(gameOptionToExecuteMock.Object);
        // Assert
        IGameOption[] nextGameOptions = gameMaster.GetOptions();
        nextGameOptions.Should()
            .Contain(nextGameOption, 
                because: "the game master should use the next additional game options in the next call to GetOptions.");
    }
}
