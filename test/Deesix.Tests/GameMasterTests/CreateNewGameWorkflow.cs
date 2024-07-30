﻿using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using FluentAssertions;
using TestKitLibrary;

namespace Deesix.Tests.GameMasterTests;

public class CreateNewGameWorkflow
{
    [Test, Explicit("OpenAI API call")]
    public async Task Should_Be_Successful()
    {
        await TestKit.Get<TestStep>().Action().CreateNewGame();
        await TestKit.Get<TestStep>().Action().ShowWorldGenres();
        await TestKit.Get<TestStep>().Action().ChooseWorldGenre();
        await TestKit.Get<TestStep>().Action().GenerateWorldSettings();
    }

    [Test]
    public async Task RepositoryTesting()
    {
        var repository = TestKit.Get<IRepository<Game>>();
        var game = new Game();
        repository.Add(game);
        repository.SaveChanges();
        var gameFromDb = repository.GetById(game.Id);
        gameFromDb.Should().Be(game);
    }
}
