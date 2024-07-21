using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameOption
{
    string Title { get; }

    int Order { get; }

    bool CanExecute(GameTurn gameTurn);

    Task<GameTurn> ExecuteAsync(GameTurn gameTurn);
}