using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.Actions.SpecificWorldGenre;

public class Created : TestFixture
{
    private Game game;
    private const string Genre = "High Fantasy";

    private IAction Action { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        Action = new SpecificWorldGenreAction(Genre, WorldRepository);
    }

    [Test]
    public void Should_Have_Correct_Title() => 
        Action.Title.Should().Be(Genre, 
            because: "action title should be the genre");
    
    [Test]
    public void Should_Have_Correct_Order() =>
        Action.Order.Should().Be(1, 
            because: "that is expected order");
}
