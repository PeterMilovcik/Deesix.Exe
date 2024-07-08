using CSharpFunctionalExtensions;
using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

internal class GameOption(string description) : IGameOption
{
    public string Description { get; } = description;

    public bool CanExecute(Maybe<Game> game) => true;
}
