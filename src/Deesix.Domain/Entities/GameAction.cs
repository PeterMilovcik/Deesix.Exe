using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public class GameAction : IGameAction
{
    public required string Name { get; init; }

    public required string ProgressName { get; init; }

    public required TimeSpan Duration { get; init; }

    public virtual bool CanExecute(GameTurn gameTurn) => true;

    public virtual Task<GameTurn> StartActionAsync(GameTurn gameTurn) => Task.FromResult(gameTurn);

    public virtual Task<GameTurn> PerformActionAsync(GameTurn gameTurn) => Task.FromResult(gameTurn);

    public virtual Task<GameTurn> FinishActionAsync(GameTurn gameTurn) => Task.FromResult(gameTurn);

    public virtual Task<GameTurn> PrepareNextTurnAsync(GameTurn gameTurn) => Task.FromResult(gameTurn);
}
