using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions.ExitGame;

public class CanExecute : ActionTestFixture<ExitAction>
{    
    [Test]
    public void Should_Return_True() => 
        Action!.CanExecute(new Turn()).Should().BeTrue();
}
