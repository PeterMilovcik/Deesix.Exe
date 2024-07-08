using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public sealed class SelectWorldGenreGameAction(string genre) : IGameAction
{
    private readonly string genre = genre ?? throw new ArgumentNullException(nameof(genre));

    public string Name => genre;

    public string ProgressName => $"Selecting {genre} genre...";

    public TimeSpan Duration => TimeSpan.Zero;

    public string GameMasterActionMessage => $"You have selected the {genre} genre for your game world.";

    public string GameMasterQuestion => "What would you like to do next?";

    public bool CanExecute(GameTurn gameTurn) =>
        gameTurn.Game.World is not null && 
        gameTurn.Game.World.Genre is null;

    public Task<Result<GameTurn>> ExecuteAsync(GameTurn gameTurn) => 
        CanExecute(gameTurn) is false
            ? Task.FromResult(Result.Failure<GameTurn>("Cannot execute SelectWorldGenreGameAction."))
            : Task.FromResult(Result.Success(gameTurn with
            {
                Game = gameTurn.Game with
                {
                    World = gameTurn.Game.World! with
                    {
                        Genre = genre
                    }
                },
                // GameMaster = gameTurn.GameMaster with
                // {
                //     StartActionMessage = $"You have selected the {genre} genre for your game world.",
                //     EndActionMessage = $"You have selected the {genre} genre for your game world.",
                //     Question = "What would you like to do next?",
                //     PossibleActions = new Collection<IGameAction>
                //     {
                //         new GenerateWorldSettingsGameAction()
                //     }
                // }
            }));

    public Task<GameTurn> FinishActionAsync(GameTurn gameTurn)
    {
        throw new NotImplementedException();
    }

    public Task<GameTurn> StartActionAsync(GameTurn gameTurn)
    {
        throw new NotImplementedException();
    }
}
