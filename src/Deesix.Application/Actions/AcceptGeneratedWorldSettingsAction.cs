﻿using Deesix.Application.Actions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class AcceptGeneratedWorldSettingsAction : IAction
{
    public string Title => "Accept generated world settings";
    public string ProgressTitle => "Accepting generated world settings...";

    public int Order => 1;

    public bool CanExecute(Turn turn) => 
        turn.Game.HasValue &&
        turn.Game.Value.World is not null &&
        turn.Game.Value.World.WorldSettings is not null &&
        (turn.LastAction is GenerateWorldSettingsAction or RegenerateWorldSettingsAction);

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        turn.Actions.Clear();
        return Task.FromResult(turn);
    }
}
