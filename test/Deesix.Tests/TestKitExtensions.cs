using Deesix.Tests.TestSteps;
using TestKitLibrary;

namespace Deesix.Tests;

public static class TestKitConfigurationExtensions
{
    public static TestKitConfiguration AddTestAspNetCoreEnvironment(this TestKitConfiguration testKitConfiguration)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        return testKitConfiguration;
    }

    public static TestKitConfiguration AddProcessAction(this TestKitConfiguration testKitConfiguration)
    {
        testKitConfiguration.Add(new ProcessActionTestSteps());
        return testKitConfiguration;
    }
}
