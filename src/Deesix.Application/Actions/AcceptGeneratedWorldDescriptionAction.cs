using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Actions;

public class AcceptGeneratedWorldDescriptionAction(string worldDescription) : IAction
{
    private readonly string worldDescription = worldDescription 
        ?? throw new ArgumentNullException(nameof(worldDescription));

    public string Title => worldDescription;
    public string ProgressTitle => "Accepting generated world description...";
    public int Order => 1;

    public bool CanExecute(Turn turn) => true;

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        turn.Game.Value.World!.Description = worldDescription;
        turn.Message = "Great choice!";
        return Task.FromResult(turn);
    }
}
