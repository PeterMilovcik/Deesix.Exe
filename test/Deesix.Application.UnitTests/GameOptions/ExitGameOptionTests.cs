using CSharpFunctionalExtensions;
using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Application.UnitTests.GameOptions;

public class ExitGameOptionTests : TestFixture
{
    private ExitGameOption? exitGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        exitGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<ExitGameOption>().FirstOrDefault();
        exitGameOption.Should().NotBeNull(because: "it is registered as a service.");
    }    

    [Test]
    public void Title_Should_Return_Start_New_Game() => 
        exitGameOption!.Title.Should().Be("Exit Game", because: "that is the expected title.");
    
    [Test]
    public void CanExecute__WithNoGame_Should_Return_True() => 
        exitGameOption!.CanExecute(new GameTurn()).Should().BeTrue();

    [Test]
    public void CanExecute_WithGame_Should_Return_True() => 
        exitGameOption!.CanExecute(new GameTurn{ Game = new Game() }).Should().BeTrue();

    [Test]
    public void Order_Should_Return_IntMaxValue() => 
        exitGameOption!.Order.Should().Be(int.MaxValue, because: "that is the expected order.");
}
