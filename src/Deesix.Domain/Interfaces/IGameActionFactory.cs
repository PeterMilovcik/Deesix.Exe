using System.Collections.ObjectModel;
using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameActionFactory
{
    Collection<IGameAction> CreateActions(GameTurn gameTurn);
}