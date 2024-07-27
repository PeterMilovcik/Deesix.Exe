using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using FluentAssertions;

namespace Deesix.Tests.GameActions.LoadGame;

[TestFixture]
public class Created : TestFixture
{
    private Game game;

    private IGameAction GameAction { get; set; }

    public override void SetUp()
    {
        base.SetUp();
        game = new Game();
        GameRepository.Add(game);
        GameAction = new LoadGameAction(game);
    }

    [Test]
    public void Should_Have_Correct_Title() => 
        GameAction.Title.Should().Be($"Load: {game.GameId} - Unknown World");
    
    [Test]
    public void Should_Have_Correct_Order() =>
        GameAction.Order.Should().Be(1, 
            because: "Load game action should be the first one");
}
