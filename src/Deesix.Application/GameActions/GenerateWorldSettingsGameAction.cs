using System.Collections.ObjectModel;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

// public class GenerateWorldSettingsAction : Action
// {
//     public GenerateWorldSettingsAction()
//     {
//         Name = "Generate World Settings";
//         ProgressName = "Generating world settings...";
//     }

//     // public override bool CanExecute(Turn turn) => 
//     //     turn.Game.World is not null
//     //         ? turn.Game.World.Genre is not null && turn.Game.World.WorldSettings is null 
//     //         : false;
    
//     // public override Task<Turn> StartActionAsync(Turn turn) =>
//     //     Task.FromResult(turn with
//     //     {
//     //         // GameMaster = turn.GameMaster with
//     //         // {
//     //         //     Message = "You've just kicked off the process of generating world settings for your amazing game world. " + 
//     //         //         "Get ready to discover the unique and immersive world that awaits you!"
//     //         // }
//     //     });
    
//     // public override Task<Turn> PerformActionAsync(Turn turn) =>
//     //     Task.FromResult(turn with
//     //     {
//     //         Game = turn.Game with
//     //         {
//     //             World = turn.Game.World! with
//     //             {
//     //                 //WorldSettings = new WorldSettings() // TODO: Implement world settings generation
//     //             }
//     //         }
//     //     });
    
//     // public override Task<Turn> FinishActionAsync(Turn turn) =>
//     //     Task.FromResult(turn with
//     //     {
//     //         // GameMaster = turn.GameMaster with
//     //         // {
//     //         //     Message = "You have successfully generated world settings for your game world.", // TODO: Describe them for player.
//     //         // }
//     //     });
    
//     // public override Task<Turn> PrepareNextTurnAsync(Turn turn) =>
//     //     Task.FromResult(turn with
//     //     {
//     //         // GameMaster = turn.GameMaster with
//     //         // {
//     //         //     Question = "What would you like to do next?",
//     //         //     PossibleActions = new Collection<IAction>
//     //         //     {
//     //         //         // new GenerateWorldDescriptionAction() // TODO: Implement world description generation
//     //         //     }
//     //         // }
//     //     });
// }