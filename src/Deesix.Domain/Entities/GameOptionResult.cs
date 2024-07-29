using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public class GameOptionResult(string message)
{
    public string NextMessage { get; } = message ?? 
        throw new ArgumentNullException(nameof(message));
    
    public string NextQuestion { get; set; } = "What would you like to do next?";

    public Result<Game> NextGameState { get; set; } = Result.Failure<Game>("No game loaded yet.");

    public List<IAction> NextAdditionalGameOptions { get; } = new List<IAction>();
}
