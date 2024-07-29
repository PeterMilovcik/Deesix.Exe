using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Spectre.Console;

namespace Deesix.Tests;

public static class Extensions
{
    public static void ToConsole(this Turn turn)
    {
        Console.WriteLine($"Message: {turn.Message}");
        Console.WriteLine($"Question: {turn.Question}");
        Console.WriteLine($"Actions: {turn.Actions.Count}");
        turn.Actions.ForEach(action => Console.WriteLine($" - {action.Title}"));
    }

    public static void ToConsole(this Maybe<Game> game)
    {
        if (game.HasNoValue)
        {
            Console.WriteLine("Game: not created");
            return;
        }
        AnsiConsole.Write(new Rule($"Game") { Justification = Justify.Left });
        Console.WriteLine("Game:");
        Console.WriteLine($"Id: {game.Value.Id}");
        Console.WriteLine($"World: {(game.Value.World != null ? "created" : "not created")}");
        if (game.Value.World != null)
        {
            Console.WriteLine($"World Genre: {game.Value.World.Genre}");
        }
        AnsiConsole.Write(new Rule());
    }
}
