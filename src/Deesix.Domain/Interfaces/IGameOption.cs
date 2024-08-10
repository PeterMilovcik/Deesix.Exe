using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IAction
{
    string Title { get; }
    string ProgressTitle { get; }
    int Order { get; }
    bool CanExecute(Turn turn);
    Task<Turn> ExecuteAsync(Turn turn);
}