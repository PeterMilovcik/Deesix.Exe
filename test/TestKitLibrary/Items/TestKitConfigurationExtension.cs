namespace TestKitLibrary;

public static class TestKitConfigurationExtension
{
    public static TestKitConfiguration AddTestStep(this TestKitConfiguration configuration)
    {
        configuration.Add(new TestStep());
        return configuration;
    }
}
