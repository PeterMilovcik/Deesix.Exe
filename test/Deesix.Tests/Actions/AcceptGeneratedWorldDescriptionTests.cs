using Deesix.Application.Actions;
using Deesix.Domain.Interfaces;

namespace Deesix.Tests.Actions;

public class AcceptGeneratedWorldDescriptionTests : ActionTestFixture
{
    private string TestDescription { get; set; } = "Test description";
    protected override string ExpectedTitle => TestDescription;
    protected override string ExpectedProgressTitle => "Accepting generated world description...";
    protected override IAction CreateAction() => new AcceptGeneratedWorldDescriptionAction(TestDescription);
}
