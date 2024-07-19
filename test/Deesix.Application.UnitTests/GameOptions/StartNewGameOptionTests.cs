using Deesix.Application.GameOptions;
using FluentAssertions;

namespace Deesix.Application.UnitTests
{
    public class StartNewGameOptionTests
    {
        private StartNewGameOption startNewGameOption;

        [SetUp]
        public void SetUp()
        {
            startNewGameOption = new StartNewGameOption();
        }

        [Test]
        public void Title_Should_Return_Start_New_Game() => 
            startNewGameOption.Title.Should().Be("Start new game", because: "that is the expected title.");
    }
}
