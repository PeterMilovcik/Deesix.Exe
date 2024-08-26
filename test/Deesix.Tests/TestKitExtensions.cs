using Deesix.Tests.TestSteps;
using TestKitLibrary;

namespace Deesix.Tests;

public static class TestKitConfigurationExtensions
{
    public static TestKitConfiguration AddDevelopmentAspNetCoreEnvironment(this TestKitConfiguration testKitConfiguration)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        return testKitConfiguration;
    }

    public static TestKitConfiguration AddProcessAction(this TestKitConfiguration testKitConfiguration)
    {
        testKitConfiguration.Add(new ProcessActionTestSteps());
        return testKitConfiguration;
    }
}
