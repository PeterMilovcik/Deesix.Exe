using System.Collections.ObjectModel;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class GameActionFactory(Collection<IGameAction> gameActions) : IGameActionFactory
{
    private readonly Collection<IGameAction> gameActions = gameActions ?? throw new ArgumentNullException(nameof(gameActions));

    public Collection<IGameAction> CreateActions(GameTurn gameTurn) => 
        new(gameActions.Where(action => action.CanExecute(gameTurn)).ToList());
}
