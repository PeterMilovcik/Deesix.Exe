using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public sealed class ExitGameAction : IGameAction
{
    public string Title => "Exit Game";

    public int Order => int.MaxValue;

    public bool CanExecute(Turn turn) => true;

    public Task<Turn> ExecuteAsync(Turn turn) => Task.FromResult(turn with 
        {
            Message = "See you later!"
        });

    public override string ToString() => Title;
}