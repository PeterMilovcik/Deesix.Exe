﻿using CSharpFunctionalExtensions;
using Spectre.Console;

namespace Deesix.Core;

public class Game
{
    public required string Id { get; set; }
    public required World World { get; set; }
    public required Character Character { get; set; }
    public DateTime CurrentTime { get; set; }

    public IEnumerable<IAction> GetAvailableActions()
    {
        var actionFactory = new ActionFactory();
        return actionFactory.GetAvailableActions(this);
    }

    public async Task<Result<string>> ProcessActionAsync(IAction action)
    {
        var result = await action.ExecuteAsync(this);
        AnsiConsole.MarkupLine(result.ToString());
        CurrentTime = CurrentTime.Add(action.Duration);
        return result;
    }
}
