using CSharpFunctionalExtensions;
using Deesix.Application.Actions;
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

    protected async Task CreateNewGame() => await Process<CreateNewAction>();
    protected async Task ShowWorldGendres() => await Process<WorldGenresAction>();
    protected async Task ChooseWorldGenre() => await Process<SpecificWorldGenreAction>();
    protected async Task GenerateWorldSettings() => await Process<GenerateWorldSettingsAction>();

    private async Task Process<TAction>() where TAction : IAction
    {
        GameMaster.Turn.ToConsole();
        Console.WriteLine($"Processing {typeof(TAction).Name}");
        await GameMaster.ProcessActionAsync(
            GameMaster.Turn.Actions.OfType<TAction>().First());
        GameMaster.Turn.Game.ToConsole();
        GameMaster.Turn.Game.Execute(game => GameRepository.SaveChanges());
    }
}
