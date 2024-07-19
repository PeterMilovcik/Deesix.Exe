using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameOption
{
    string Title { get; }

    bool CanExecute(Maybe<Game> game);

    Task<GameOptionResult> ExecuteAsync(Maybe<Game> game);
}