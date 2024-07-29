using CSharpFunctionalExtensions;
using Deesix.Application.GameActions;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests;

public class GameMasterTestFixture : TestFixture
{
    protected IGameMaster GameMaster { get; private set; }

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        GameMaster = Services.GetRequiredService<IGameMaster>();
        GameMaster.Should().NotBeNull(because: "the game master should be registered in the services");
    }

    protected async Task CreateNewGame() => await Process<CreateNewGameAction>();
    protected async Task ShowWorldGendres() => await Process<WorldGenresGameOption>();
    protected async Task ChooseWorldGenre() => await Process<SpecificWorldGenreGameAction>();

    private async Task Process<TGameAction>() where TGameAction : IGameAction
    {
        GameMaster.Turn.ToConsole();
        Console.WriteLine($"Processing {typeof(TGameAction).Name}");
        await GameMaster.ProcessGameActionAsync(
            GameMaster.Turn.GameActions.OfType<TGameAction>().First());
        GameMaster.Turn.Game.ToConsole();
        GameMaster.Turn.Game.Execute(game => GameRepository.SaveChanges());
    }
}
