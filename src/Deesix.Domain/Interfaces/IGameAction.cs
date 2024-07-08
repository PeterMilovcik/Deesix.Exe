using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameAction
{
    string Name { get; }
    string ProgressName { get; }
    TimeSpan Duration { get; }
    bool CanExecute(GameTurn gameTurn);
    Task<GameTurn> StartActionAsync(GameTurn gameTurn);
    Task<GameTurn> FinishActionAsync(GameTurn gameTurn);
}