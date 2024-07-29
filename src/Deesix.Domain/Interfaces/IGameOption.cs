using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameAction
{
    string Title { get; }
    int Order { get; }
    bool CanExecute(Turn turn);
    Task<Turn> ExecuteAsync(Turn turn);
}