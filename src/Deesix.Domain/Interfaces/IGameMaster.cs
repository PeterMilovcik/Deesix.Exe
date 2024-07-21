using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameMaster
{
    GameTurn GameTurn { get; }
    Task ProcessOptionAsync(IGameOption option);
}
