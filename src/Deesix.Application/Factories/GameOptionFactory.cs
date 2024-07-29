using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Factories;

public class GameOptionFactory(IEnumerable<IAction> gameOptions) : IGameOptionFactory
{
    private readonly IEnumerable<IAction> gameOptions = gameOptions;

    public List<IAction> CreateGameOptions(Turn turn) => 
        gameOptions
            .OrderBy(option => option.Order)
            .ToList();
}
