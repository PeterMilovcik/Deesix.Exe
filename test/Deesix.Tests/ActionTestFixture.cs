using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests;

public abstract class ActionTestFixture<TAction> : ActionTestFixture where TAction : IAction
{
    protected override IAction CreateAction() => 
        Services.GetRequiredService<IEnumerable<IAction>>().OfType<TAction>().Single();
}

public abstract class ActionTestFixture : TestFixture
{
    protected IAction Action { get; set; }
    protected abstract string ExpectedTitle { get; }
    protected abstract string ExpectedProgressTitle { get; }
    protected virtual int ExpectedOrder => 1;
    protected abstract IAction CreateAction();
    
    public override void SetUp()
    {
        base.SetUp();
        Action = CreateAction();
        Action.Should().NotBeNull(
            because: "the action should be registered as a service.");
    }

    [Test]
    public void Title() => 
        Action!.Title.Should().Be(ExpectedTitle, 
            because: "that is the expected title.");
    
    [Test]
    public void ProgressTitle() => 
        Action!.ProgressTitle.Should().Be(ExpectedProgressTitle, 
            because: "that is the expected progress title.");
    
    [Test]
    public void Order() => 
        Action!.Order.Should().Be(ExpectedOrder, 
            because: "that is the expected order.");
}
