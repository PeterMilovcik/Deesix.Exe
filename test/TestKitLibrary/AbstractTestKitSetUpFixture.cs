namespace TestKitLibrary;

public abstract class AbstractTestKitSetUpFixture
{
    public virtual void OneTimeSetUp()
    {
        var configuration = CreateTestKitConfiguration();
        Configure(configuration);
        TestKit.Set(configuration.All());
    }

    protected virtual TestKitConfiguration CreateTestKitConfiguration() => new TestKitConfiguration();

    protected abstract void Configure(TestKitConfiguration testKitConfiguration);

    public virtual void OneTimeTearDown()
    {
    }
}
