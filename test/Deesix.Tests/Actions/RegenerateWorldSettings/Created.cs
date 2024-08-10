using Deesix.Application;
using FluentAssertions;

namespace Deesix.Tests.Actions.RegenerateWorldSettings;

public class Created : ActionTestFixture<RegenerateWorldSettingsAction>
{
    [Test]
    public void Title_Should_Return_Generate_World_Settings() => 
        Action!.Title.Should().Be("Regenerate World Settings", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_2() => 
        Action!.Order.Should().Be(2, 
            because: "that is the expected order.");
}
