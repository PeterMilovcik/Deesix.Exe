using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.Actions.LoadGame;

[TestFixture]
public class Created : TestFixture
{
    private Game game;

    private IAction Action { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        Action = new LoadAction(game);
    }

    [Test]
    public void Should_Have_Correct_Title() => 
        Action.Title.Should().Be($"Load: {game.GameId} - Unknown World");
    
    [Test]
    public void Should_Have_Correct_Order() =>
        Action.Order.Should().Be(1, 
            because: "Load game action should be the first one");
}
