using CSharpFunctionalExtensions;

namespace Deesix.Core;

public class ExploreAction : IAction
{
    public ActionName Name => ActionName.Create("Explore");
    public TimeSpan Duration => TimeSpan.FromSeconds(3);

    public bool CanExecute(Game game) => 
        game.Character.CurrentLocation is not null 
            ? game.Character.CurrentLocation.Explored < game.Character.CurrentLocation.Size
            : false;

    public Task<Result> ExecuteAsync(Game game)
    {
        return Task.FromResult(Result.Success());
    }
}
