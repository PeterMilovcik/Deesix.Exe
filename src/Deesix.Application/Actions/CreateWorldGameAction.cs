﻿namespace Deesix.Application;

// public sealed class CreateWorldAction : IAction
// {
//     public string Name => "Create World";

//     public string ProgressName => "Creating World...";

//     public string GameMasterActionMessage => "You have decided to create a new world for a game. Prepare yourself for an epic adventure!";

//     public string GameMasterQuestion => "What genre of game world would you like to play?";

//     public TimeSpan Duration => TimeSpan.Zero;

//     public bool CanExecute(Turn turn) => turn.Game.World is null;

//     public Task<Result<Turn>> ExecuteAsync(Turn turn) => 
//         Task.FromResult(Result.Success(turn with
//         {
//             Game = turn.Game with
//             {
//                 World = new World()
//             },
//             // GameMaster = (turn.GameMaster with
//             // {
//             //     StartActionMessage = "You have decided to create a new world for a game.",
//             //     EndActionMessage = "You have decided to create a new world for a game.",
//             //     Question = "What genre of game world would you like to play?",
//             //     PossibleActions =
//             //     [
//             //         new SelectWorldGenreAction("Fantasy"),
//             //         new SelectWorldGenreAction("Sci-Fi"),
//             //         new SelectWorldGenreAction("Post-Apocalyptic"),
//             //         new SelectWorldGenreAction("Cyberpunk"),
//             //         new SelectWorldGenreAction("Steampunk"),
//             //         new SelectWorldGenreAction("Historical"),
//             //         new SelectWorldGenreAction("Wilderness Survival")
//             //     ]
//             // })
//         }));

//     public Task<Turn> FinishActionAsync(Turn turn)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<Turn> StartActionAsync(Turn turn)
//     {
//         throw new NotImplementedException();
//     }
// }
