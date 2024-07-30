using Deesix.Tests.TestSteps;
using TestKitLibrary;

namespace Deesix.Tests;

public static class TestStepExtensions
{
    public static ProcessActionTestSteps Action(this TestStep testStep) => 
        TestKit.Get<ProcessActionTestSteps>();
}
