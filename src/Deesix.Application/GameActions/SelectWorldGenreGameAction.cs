using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

// public sealed class SelectWorldGenreGameAction(string genre) : IGameAction
// {
//     private readonly string genre = genre ?? throw new ArgumentNullException(nameof(genre));

//     public string Name => genre;

//     public string ProgressName => $"Selecting {genre} genre...";

//     public TimeSpan Duration => TimeSpan.Zero;

//     public string GameMasterActionMessage => $"You have selected the {genre} genre for your game world.";

//     public string GameMasterQuestion => "What would you like to do next?";

//     public bool CanExecute(Turn turn) =>
//         turn.Game.World is not null && 
//         turn.Game.World.Genre is null;

//     public Task<Result<Turn>> ExecuteAsync(Turn turn) => 
//         CanExecute(turn) is false
//             ? Task.FromResult(Result.Failure<Turn>("Cannot execute SelectWorldGenreGameAction."))
//             : Task.FromResult(Result.Success(turn with
//             {
//                 Game = turn.Game with
//                 {
//                     World = turn.Game.World! with
//                     {
//                         Genre = genre
//                     }
//                 },
//                 // GameMaster = turn.GameMaster with
//                 // {
//                 //     StartActionMessage = $"You have selected the {genre} genre for your game world.",
//                 //     EndActionMessage = $"You have selected the {genre} genre for your game world.",
//                 //     Question = "What would you like to do next?",
//                 //     PossibleActions = new Collection<IGameAction>
//                 //     {
//                 //         new GenerateWorldSettingsGameAction()
//                 //     }
//                 // }
//             }));

//     public Task<Turn> FinishActionAsync(Turn turn)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<Turn> StartActionAsync(Turn turn)
//     {
//         throw new NotImplementedException();
//     }
// }
