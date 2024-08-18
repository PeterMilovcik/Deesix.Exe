using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using FluentAssertions;

namespace Deesix.Tests.Actions;

public class ExitActionTests : ActionTestFixture<ExitAction>
{
    protected override string ExpectedTitle => "Exit game";
    protected override string ExpectedProgressTitle => "Exiting game...";
    protected override int ExpectedOrder => int.MaxValue;
    
    [Test]
    public void CanExecute_Should_Return_True() => 
        Action!.CanExecute(new Turn()).Should().BeTrue();
}
