using CSharpFunctionalExtensions;
using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Deesix.Application.UnitTests.GameOptions
{
    public class StartNewGameOptionTests : TestFixture
    {
        private StartNewGameOption? startNewGameOption;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            startNewGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<StartNewGameOption>().FirstOrDefault();
            startNewGameOption.Should().NotBeNull(because: "it is registered as a service.");
        }

        [Test]
        public void Title_Should_Return_Start_New_Game() => 
            startNewGameOption!.Title.Should().Be("Start a new game", because: "that is the expected title.");

        [Test]
        public void CanExecute_Should_Return_True_When_Game_Has_No_Value() =>
            startNewGameOption!.CanExecute(Maybe<Game>.None).Should().BeTrue();
        
        [Test]
        public void CanExecute_Should_Return_False_When_Game_Has_Value() =>
            startNewGameOption!.CanExecute(Maybe<Game>.From(new Game())).Should().BeFalse();

        [Test]
        public async Task ExecuteAsync_Should_Return_GameOptionResult_With_Game_When_Game_Has_No_Value()
        {
            // Arrange
            var noGame = Maybe<Game>.None;
            var createdGame = new Game();
            GameRepositoryMock.Setup(x => x.Add(It.IsAny<Game>())).Returns(createdGame);
            // Act
            var gameOptionResult = await startNewGameOption!.ExecuteAsync(noGame);
            // Assert
            gameOptionResult.NextGameState.Should().NotBeNull();
            gameOptionResult.NextMessage.Should().Be("Game started successfully! Get ready for an exciting adventure!");
        }
    }
}
