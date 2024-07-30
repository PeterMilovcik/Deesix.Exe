namespace Deesix.Tests;

public abstract class AbstractTestKitSetUpFixture
{
    public virtual void OneTimeSetUp()
    {
        var configuration = CreateTestKitConfiguration();
        SetUp(configuration);
        TestKit.Set(configuration.All());
    }

    protected virtual TestKitConfiguration CreateTestKitConfiguration() => new TestKitConfiguration();

    protected abstract void SetUp(TestKitConfiguration testKitConfiguration);

    public virtual void OneTimeTearDown()
    {
    }
}
