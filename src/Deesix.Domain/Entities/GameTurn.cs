using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public record Turn
{
    public Maybe<Game> Game { get; set; } = Maybe<Game>.None;
    public string Message { get; set; } = "Welcome to Deesix!";
    public string Question { get; set; } = "What would you like to do?";
    public List<IAction> Actions { get; set; } = [];
    public IAction LastAction { get; set; } = null!;
}