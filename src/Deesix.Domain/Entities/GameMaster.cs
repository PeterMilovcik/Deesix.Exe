using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster(IEnumerable<IGameOption> gameOptions) : IGameMaster
{
    private readonly IEnumerable<IGameOption> gameOptions = gameOptions ?? Array.Empty<IGameOption>();
    private string message = "Message from the Game Master";
    public Maybe<Game> Game { get; } = Maybe<Game>.None;

    // public required GameTurn CurrentTurn { get; private set; }

    // public bool CanAdvanceTurn => CurrentTurn.SelectedAction != null;

    // public Task AdvanceTurnAsync()
    // {
    //     // CurrentTurn = await CurrentTurn.SelectedAction.StartActionAsync(CurrentTurn);
    //     // CurrentTurn = await CurrentTurn.SelectedAction.PerformActionAsync(CurrentTurn);
    //     // CurrentTurn = await CurrentTurn.SelectedAction.CompleteActionAsync(CurrentTurn);
    // }
    public string GetMessage() => message;

    public IGameOption[] GetOptions()
    {
        return gameOptions.ToArray();
    }

    public string GetQuestion() => "Question from the Game Master";

    public Task ProcessOptionAsync(IGameOption option)
    {
        var gameOptionResult = new GameOptionResult($"Option '{option.Description}' was processed.");
        message = gameOptionResult.ResultMessage;
        return Task.CompletedTask;
    }
}
