using System;
using Deesix.Application;
using FluentAssertions;

namespace Deesix.Tests.Actions.AcceptGeneratedWorldSettings;

public class Created : ActionTestFixture<AcceptGeneratedWorldSettingsAction>
{
    [Test]
    public void Title_Should_Return_Generate_World_Settings() => 
        Action!.Title.Should().Be("Accept generated world settings", 
            because: "that is the expected title.");

    [Test]
    public void ProgressTitle_Should_Return_Accepting_Generated_World_Settings() => 
        Action!.ProgressTitle.Should().Be("Accepting generated world settings...", 
            because: "that is the expected progress title.");

    [Test]
    public void Order_Should_Return_1() => 
        Action!.Order.Should().Be(1, 
            because: "that is the expected order.");
}
