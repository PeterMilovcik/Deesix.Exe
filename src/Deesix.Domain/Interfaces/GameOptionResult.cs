using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public class GameOptionResult(string message, Result<Game> game)
{
    public string Message { get; } = message ?? 
        throw new ArgumentNullException(nameof(message));

    public Result<Game> Game { get; } = game;
}
