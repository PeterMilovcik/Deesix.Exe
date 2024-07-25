using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameOptionFactory
{
    List<IGameAction> CreateGameOptions(GameTurn gameTurn);
}
