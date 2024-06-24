using CSharpFunctionalExtensions;

namespace Deesix.Core;

public class ExploreAction : IAction
{
    public ActionName Name => ActionName.Create("Explore");
    public TimeSpan Duration => TimeSpan.FromSeconds(3);
    public Task<Result> ExecuteAsync(Game game) => Task.FromResult(Result.Success());
}
