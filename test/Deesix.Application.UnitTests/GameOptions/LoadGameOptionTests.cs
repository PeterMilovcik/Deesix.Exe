using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Deesix.Application.GameOptions;
using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests;

[TestFixture]
public class LoadGameOptionTests : TestFixture
{
    private LoadGameOption? loadGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        loadGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<LoadGameOption>().FirstOrDefault();
        loadGameOption.Should().NotBeNull(because: "it is registered as a service.");
    }

    [Test]
    public void CanExecute_WhenGameHasValue_ReturnsFalse() => 
        loadGameOption!.CanExecute(Maybe<Game>.From(new Game())).Should().BeFalse(because: "a game is already loaded.");

    [Test]
    public void CanExecute_WhenGameHasNoValueAndRepositoryHasNoGames_ReturnsFalse() => 
        loadGameOption!.CanExecute(Maybe<Game>.None).Should().BeFalse(because: "there are no games in the repository.");

    [Test]
    public void CanExecute_WhenGameHasNoValueAndRepositoryHasGames_ReturnsTrue()
    {
        // Arrange
        GameRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Game> { new Game() });
        // Act & Assert
        loadGameOption!.CanExecute(Maybe<Game>.None).Should().BeTrue(because: "there are games in the repository.");
    }
}
