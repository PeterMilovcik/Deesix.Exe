using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameAction
{
    string Title { get; }
    int Order { get; }
    bool CanExecute(GameTurn gameTurn);
    Task<GameTurn> ExecuteAsync(GameTurn gameTurn);
}