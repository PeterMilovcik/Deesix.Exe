using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IAction
{
    ActionName Name { get; }
    ActionName ProgressName { get; }
    TimeSpan Duration { get; }
    bool CanExecute(Game game);
    Task<Result<string>> ExecuteAsync(Game game);
}