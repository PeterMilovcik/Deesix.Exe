using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.GameOptions.CreateNewGameOptionTests;

public class CanExecute : TestFixture
{
    private CreateNewGameOption? createNewGameOption;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        createNewGameOption = Services
            .GetRequiredService<IEnumerable<IGameOption>>()
            .OfType<CreateNewGameOption>()
            .FirstOrDefault();
        createNewGameOption.Should().NotBeNull(
            because: "it is registered as a service.");
    }

    [Test]
    public void Should_Return_True_When_Game_Has_No_Value() =>
        createNewGameOption!.CanExecute(new GameTurn()).Should().BeTrue(
            because: "the game has no value.");
    
    [Test]
    public void Should_Return_False_When_Game_Has_Value() =>
        createNewGameOption!.CanExecute(new GameTurn { Game = new Game()})
            .Should().BeFalse(
                because: "the game has already a value.");

    [Test]
    public void Should_Return_False_When_LastOption_Is_LoadGamesOption() => 
        createNewGameOption!.CanExecute(
            new GameTurn { LastOption = new LoadGamesOption(GameRepository) })
                .Should().BeFalse(
                    because: "the last option is LoadGamesOption.");
}
