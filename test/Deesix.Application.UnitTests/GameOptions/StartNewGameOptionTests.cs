using CSharpFunctionalExtensions;
using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Application.UnitTests
{
    public class StartNewGameOptionTests
    {
        private StartNewGameOption startNewGameOption;

        [SetUp]
        public void SetUp() => startNewGameOption = new StartNewGameOption();

        [Test]
        public void Title_Should_Return_Start_New_Game() => 
            startNewGameOption.Title.Should().Be("Start new game", because: "that is the expected title.");

        [Test]
        public void CanExecute_Should_Return_True_When_Game_Has_No_Value() =>
            startNewGameOption.CanExecute(Maybe<Game>.None).Should().BeTrue();
        
        [Test]
        public void CanExecute_Should_Return_False_When_Game_Has_Value() =>
            startNewGameOption.CanExecute(Maybe<Game>.From(new Game())).Should().BeFalse();
        
        [Test]
        public async Task ExecuteAsync_Should_Return_GameOptionResult_With_Title() =>
            (await startNewGameOption.ExecuteAsync(Maybe<Game>.None)).Message.Should().Be("Start new game");
    }
}
