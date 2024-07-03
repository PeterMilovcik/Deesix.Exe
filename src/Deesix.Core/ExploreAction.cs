using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Core;

public class ExploreAction : IAction
{
    public ActionName Name => ActionName.Create("Explore location");
    public ActionName ProgressName => ActionName.Create("Exploring location...");
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
                var outcomeGenerator = GetRandomOutcomeGenerator(sum);
                var outcome = outcomeGenerator.GenerateOutcome(game, sum);
                outcome.Execute(game);
                return Result.Success(outcome.Description);
            }
            else
            {
                return Result.Failure<string>("You didn't find anything of interest.");
            }
        }
        return Result.Failure<string>("You didn't find anything of interest.");
    }

    private IOutcomeGenerator GetRandomOutcomeGenerator(int sum)
    {
        var random = new Random();
        var index = random.Next(0, 2);
        return index switch
        {
            0 => new NewPathOutcomeGenerator(),
            _ => new NothingInterestingOutcomeGenerator(),
        };
    }
}

public class NothingInterestingOutcomeGenerator : IOutcomeGenerator
{
    public IOutcome GenerateOutcome(Game game, int rollSum) => new NothingInterestingOutcome();
}

public class NothingInterestingOutcome : IOutcome
{
    public string Description => "You didn't find anything of interest.";

    public void Execute(Game game)
    {
    }
}

public class NewPathOutcomeGenerator : IOutcomeGenerator
{
    public IOutcome GenerateOutcome(Game game, int rollSum)
    {
        // TODO: Implement logic to generate a new path
        return new NewPathOutcome();
    }
}

public class NewPathOutcome : IOutcome
{
    public string Description => "You found a new path!";

    public void Execute(Game game)
    {       
    }
}

public interface IOutcomeGenerator
{
    IOutcome GenerateOutcome(Game game, int rollSum);
}

public interface IOutcome
{
    string Description { get; }
    void Execute(Game game);
}