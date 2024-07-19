using Deesix.Application.GameOptions;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Infrastructure.UnitTests;

[TestFixture]
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
}
