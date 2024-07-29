using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameMaster
{
    Turn Turn { get; }
    Task ProcessActionAsync(IAction option);
}
