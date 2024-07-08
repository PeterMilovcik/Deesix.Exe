using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster(IEnumerable<IGameOption> gameOptions) : IGameMaster
{
    private readonly IEnumerable<IGameOption> gameOptions = gameOptions ?? Array.Empty<IGameOption>();
    private string message = "Message from the Game Master";
    public Maybe<Game> Game { get; } = Maybe<Game>.None;
    
    public string GetMessage() => message;

    public IGameOption[] GetOptions() => gameOptions.ToArray();

    public string GetQuestion() => "What are you going to do?";

    public async Task ProcessOptionAsync(IGameOption option)
    {
        var result = await option.ExecuteAsync(Game);
        message = result.Message;
    }
}
