using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.ConsoleUI.UnitTests;

public class GameRepositoryTests : TestFixture
{
    private IRepository<Game> repository;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        repository = Services.GetRequiredService<IRepository<Game>>();
    }

    [Test]
    public void AddGame()
    {
        DateTime currentTime = DateTime.Now;
        var game = new Game
        {
            CurrentTime = currentTime
        };

        var addedGame = repository.Add(game);
        var result = repository.GetById(addedGame.Id);
        result.Should().BeEquivalentTo(addedGame);
        addedGame.CurrentTime.Should().Be(currentTime);
    }
}