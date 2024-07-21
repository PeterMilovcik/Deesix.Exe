using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster(IEnumerable<IGameOption> gameOptions) : IGameMaster
{
    private readonly IEnumerable<IGameOption> initialGameOptions = gameOptions ?? Array.Empty<IGameOption>();
    private List<IGameOption> temporaryGameOptions = new List<IGameOption>();

    private string message = 
        "Welcome, adventurer! I am here to guide you through this game. " + 
        "As the game master, I have prepared a thrilling adventure filled with challenges and mysteries. ";
    
    private string question = "Are you ready to embark on this epic journey?";

    public Maybe<Game> Game { get; private set; } = Maybe<Game>.None;
    
    public string GetMessage() => message;

    public IGameOption[] GetOptions() => 
        initialGameOptions.Concat(temporaryGameOptions)
            .Where(gameOption => gameOption.CanExecute(Game)).ToArray();

    public string GetQuestion() => question;

    public async Task ProcessOptionAsync(IGameOption option)
    {
        var result = await option.ExecuteAsync(Game);
        message = result.NextMessage;
        question = result.NextQuestion;
        if (result.NextGameState.IsSuccess)
        {
            Game = Maybe.From(result.NextGameState.Value);
        }

        temporaryGameOptions = result.NextAdditionalGameOptions;
    }
}
