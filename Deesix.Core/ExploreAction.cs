using CSharpFunctionalExtensions;

namespace Deesix.Core;

public class ExploreAction : IAction
{
    public ActionName Name => ActionName.Create("Explore");
    public ActionName ProgressName => ActionName.Create("Exploring...");
    public TimeSpan Duration => TimeSpan.FromSeconds(3);

    public bool CanExecute(Game game) => 
        game.Character.CurrentLocation is not null 
            ? game.Character.CurrentLocation.Explored < game.Character.CurrentLocation.Size
            : false;

    public async Task<Result> ExecuteAsync(Game game)
    {
        await Task.Delay(Duration);
        return Result.Success();
    }
}
