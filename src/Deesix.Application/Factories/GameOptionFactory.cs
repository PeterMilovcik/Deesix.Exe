using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Factories;

public class GameOptionFactory(IEnumerable<IGameAction> gameOptions) : IGameOptionFactory
{
    private readonly IEnumerable<IGameAction> gameOptions = gameOptions;

    public List<IGameAction> CreateGameOptions(GameTurn gameTurn) => 
        gameOptions
            .OrderBy(option => option.Order)
            .ToList();
}
