using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameOptionFactory
{
    List<IGameOption> CreateGameOptions(GameTurn gameTurn);
}
