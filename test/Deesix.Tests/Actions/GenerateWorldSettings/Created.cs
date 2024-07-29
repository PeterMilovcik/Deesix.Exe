using Deesix.Application.Actions;
using FluentAssertions;

namespace Deesix.Tests.Actions.GenerateWorldSettings;

public class Created : ActionTestFixture<GenerateWorldSettingsAction>
{
    [Test]
    public void Title_Should_Return_Generate_World_Settings() => 
        Action!.Title.Should().Be("Generate World Settings", 
            because: "that is the expected title.");

    [Test]
    public void Order_Should_Return_1() => 
        Action!.Order.Should().Be(1, 
            because: "that is the expected order.");
}
