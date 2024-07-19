using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public sealed class ExitGameOption : IGameOption
{
    public string Title => "Exit the game";

    public bool CanExecute(Maybe<Game> game) => true;

    public Task<GameOptionResult> ExecuteAsync(Maybe<Game> game) => 
        Task.FromResult(new GameOptionResult("See you next time! Have a nice day!", game.Value));

    public override string ToString() => Title;
}