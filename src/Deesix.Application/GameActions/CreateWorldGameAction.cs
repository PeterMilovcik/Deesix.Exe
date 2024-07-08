using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public sealed class CreateWorldGameAction : IGameAction
{
    public string Name => "Create World";

    public string ProgressName => "Creating World...";

    public string GameMasterActionMessage => "You have decided to create a new world for a game. Prepare yourself for an epic adventure!";

    public string GameMasterQuestion => "What genre of game world would you like to play?";

    public TimeSpan Duration => TimeSpan.Zero;

    public bool CanExecute(GameTurn gameTurn) => gameTurn.Game.World is null;

    public Task<Result<GameTurn>> ExecuteAsync(GameTurn gameTurn) => 
        Task.FromResult(Result.Success(gameTurn with
        {
            Game = gameTurn.Game with
            {
                World = new World()
            },
            // GameMaster = (gameTurn.GameMaster with
            // {
            //     StartActionMessage = "You have decided to create a new world for a game.",
            //     EndActionMessage = "You have decided to create a new world for a game.",
            //     Question = "What genre of game world would you like to play?",
            //     PossibleActions =
            //     [
            //         new SelectWorldGenreGameAction("Fantasy"),
            //         new SelectWorldGenreGameAction("Sci-Fi"),
            //         new SelectWorldGenreGameAction("Post-Apocalyptic"),
            //         new SelectWorldGenreGameAction("Cyberpunk"),
            //         new SelectWorldGenreGameAction("Steampunk"),
            //         new SelectWorldGenreGameAction("Historical"),
            //         new SelectWorldGenreGameAction("Wilderness Survival")
            //     ]
            // })
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
