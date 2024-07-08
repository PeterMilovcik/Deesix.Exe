using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class WelcomeGameOption : IGameOption
{
    public string Description => "Welcome to the game!";

    public bool CanExecute(Maybe<Game> game) => game.HasNoValue;
}