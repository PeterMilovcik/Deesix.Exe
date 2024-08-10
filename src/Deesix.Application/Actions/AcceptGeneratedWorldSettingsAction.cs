using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class AcceptGeneratedWorldSettingsAction : IAction
{
    public string Title => "Accept Generated World Settings";

    public int Order => 1;

    public bool CanExecute(Turn turn) => 
        turn.Game.HasValue &&
        turn.Game.Value.World is not null &&
        turn.Game.Value.World.WorldSettings is not null &&
        (turn.LastAction is GenerateWorldSettingsAction or RegenerateWorldSettingsAction);

    public Task<Turn> ExecuteAsync(Turn turn) => Task.FromResult(turn);
}
