using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class SpecificWorldGenreGameOption(string genre) : IGameOption
{
    public string Title => genre;

    public int Order => 1;

    public bool CanExecute(GameTurn gameTurn)
    {
        throw new NotImplementedException();
    }

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        throw new NotImplementedException();
    }
}
