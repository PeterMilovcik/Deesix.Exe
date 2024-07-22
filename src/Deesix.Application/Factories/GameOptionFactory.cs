using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Factories;

public class GameOptionFactory(IEnumerable<IGameOption> gameOptions) : IGameOptionFactory
{
    private readonly IEnumerable<IGameOption> gameOptions = gameOptions;

    public List<IGameOption> CreateGameOptions(GameTurn gameTurn) => 
        gameOptions
            .Where(option => option.CanExecute(gameTurn))
            .OrderBy(option => option.Order)
            .ToList();
}
