using Deesix.Application.GameActions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.GameActions.ExitGame;

public class CanExecute : GameActionTestFixture<ExitGameAction>
{    
    [Test]
    public void Should_Return_True() => 
        GameAction!.CanExecute(new Turn()).Should().BeTrue();
}
