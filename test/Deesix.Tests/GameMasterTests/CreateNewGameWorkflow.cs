using TestKitLibrary;

namespace Deesix.Tests.GameMasterTests;

public class CreateNewGameWorkflow
{
    [Test, Explicit("OpenAI API call")]
    public async Task Should_Be_Successful()
    {
        await TestKit.Get<TestStep>().Action().CreateNewGame();
        await TestKit.Get<TestStep>().Action().ShowWorldGenres();
        await TestKit.Get<TestStep>().Action().ChooseWorldGenre();
        await TestKit.Get<TestStep>().Action().GenerateWorldSettings();
    }
}
