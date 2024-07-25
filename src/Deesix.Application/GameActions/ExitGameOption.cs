using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameActions;

public sealed class ExitGameAction : IGameAction
{
    public string Title => "Exit Game";

    public int Order => int.MaxValue;

    public bool CanExecute(GameTurn gameTurn) => true;

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn) => Task.FromResult(gameTurn with 
        {
            Message = "See you later!"
        });

    public override string ToString() => Title;
}