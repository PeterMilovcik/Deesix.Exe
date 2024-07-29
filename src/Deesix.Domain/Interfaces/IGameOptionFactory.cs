using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameOptionFactory
{
    List<IAction> CreateGameOptions(Turn turn);
}
