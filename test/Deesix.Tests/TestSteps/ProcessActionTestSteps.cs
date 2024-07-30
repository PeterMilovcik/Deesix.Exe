using CSharpFunctionalExtensions;
using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;
using TestKitLibrary;

namespace Deesix.Tests.TestSteps;

public class ProcessActionTestSteps
{
    public async Task CreateNewGame()
    {
        var action = TestKit.Get<IGameMaster>().Turn.Actions.OfType<CreateNewAction>().First();
        await Process(action);
        TestKit.Get<IGameMaster>().Turn.Game.HasValue.Should().BeTrue(
            because: "the game should be created");
    }

    public async Task ShowWorldGenres()
    {
        var action = TestKit.Get<IGameMaster>().Turn.Actions
            .OfType<WorldGenresAction>().First();
        await Process(action);
        TestKit.Get<IGameMaster>().Turn.Actions.Should().Contain(action => action.Title.StartsWith("High Fantasy"), 
            because: "the world genre should be available");
    }

    public async Task ChooseWorldGenre(string genre = "High Fantasy")
    {
        var action = TestKit.Get<IGameMaster>().Turn.Actions
            .OfType<SpecificWorldGenreAction>().First(a => a.Title.StartsWith(genre));
        await Process(action);
        TestKit.Get<IGameMaster>().Turn.Game.Value.World.Should().NotBeNull(
            because: "the world should be created");
    }

    public async Task GenerateWorldSettings()
    {
        var action = TestKit.Get<IGameMaster>().Turn.Actions
            .OfType<GenerateWorldSettingsAction>().First();
        await Process(action);
        TestKit.Get<IGameMaster>().Turn.Game.Value.World!.WorldSettings.Should().NotBeNull(
            because: "the world settings should be created");
    }

    private async Task Process<TAction>(TAction action) where TAction : IAction
    {
        TestKit.Get<IGameMaster>().Turn.ToConsole();
        Console.WriteLine($"Processing {typeof(TAction).Name}");
        await TestKit.Get<IGameMaster>().ProcessActionAsync(action);
        TestKit.Get<IGameMaster>().Turn.Game.ToConsole();
        TestKit.Get<IGameMaster>().Turn.Game.Execute(game => TestKit.Get<IRepository<Game>>().SaveChanges());
    }
}
