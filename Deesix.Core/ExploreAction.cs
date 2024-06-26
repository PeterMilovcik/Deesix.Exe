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

    public async Task<Result<string>> ExecuteAsync(Game game)
    {
        await Task.Delay(Duration);
        var roll = game.Character.Skills.Exploration.Roll();
        var sum = roll.Sum();
        if (game.Character.CurrentLocation is not null)
        {
            var explored = game.Character.CurrentLocation.Explore(sum);
            if (explored > 0)
            {
                var outcome = "something interesting"; // TODO: Implement exploration outcomes
                return Result.Success($"You found {outcome}.");
            }
            else
            {
                return Result.Failure<string>("You didn't find anything of interest.");
            }
        }
        return Result.Failure<string>("You didn't find anything of interest.");
    }
}
