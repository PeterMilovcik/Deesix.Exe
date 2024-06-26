using CSharpFunctionalExtensions;

namespace Deesix.Core;

public interface IAction
{
    ActionName Name { get; }
    ActionName ProgressName { get; }
    TimeSpan Duration { get; }
    bool CanExecute(Game game);
    Task<Result> ExecuteAsync(Game game);
}