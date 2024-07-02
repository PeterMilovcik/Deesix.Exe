using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Core;

public class ActionFactory
{
    private readonly List<IAction> allActions = new List<IAction>
    {
        new ExploreAction(),
    };

    public IEnumerable<IAction> GetAvailableActions(Game game) =>
        allActions.Where(action => action.CanExecute(game));
}
