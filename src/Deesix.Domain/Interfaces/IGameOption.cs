using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameOption
{
    string Description { get; }

    bool CanExecute(Maybe<Game> game);

    Task<GameOptionResult> ExecuteAsync(Maybe<Game> game);
}