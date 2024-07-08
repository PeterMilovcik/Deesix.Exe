using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public sealed class WelcomeGameOption : IGameOption
{
    public string Description => "Welcome to the game!";

    public bool CanExecute(Maybe<Game> game) => game.HasNoValue;

    public Task<GameOptionResult> ExecuteAsync(Maybe<Game> game) => 
        Task.FromResult(new GameOptionResult(Description));
}
