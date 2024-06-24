using CSharpFunctionalExtensions;

namespace Deesix.Core;

public interface IAction
{
    ActionName Name { get; }
    TimeSpan Duration { get; }
    Task<Result> ExecuteAsync(Game game);
}