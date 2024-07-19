using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public class GameOptionResult(string message, Game game)
{
    public string Message { get; } = message ?? 
        throw new ArgumentNullException(nameof(message));

    public Game Game { get; } = game ?? 
        throw new ArgumentNullException(nameof(game));
}
