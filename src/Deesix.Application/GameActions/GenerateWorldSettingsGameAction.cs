using System.Collections.ObjectModel;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

// public class GenerateWorldSettingsGameAction : GameAction
// {
//     public GenerateWorldSettingsGameAction()
//     {
//         Name = "Generate World Settings";
//         ProgressName = "Generating world settings...";
//     }

//     // public override bool CanExecute(GameTurn gameTurn) => 
//     //     gameTurn.Game.World is not null
//     //         ? gameTurn.Game.World.Genre is not null && gameTurn.Game.World.WorldSettings is null 
//     //         : false;
    
//     // public override Task<GameTurn> StartActionAsync(GameTurn gameTurn) =>
//     //     Task.FromResult(gameTurn with
//     //     {
//     //         // GameMaster = gameTurn.GameMaster with
//     //         // {
//     //         //     Message = "You've just kicked off the process of generating world settings for your amazing game world. " + 
//     //         //         "Get ready to discover the unique and immersive world that awaits you!"
//     //         // }
//     //     });
    
//     // public override Task<GameTurn> PerformActionAsync(GameTurn gameTurn) =>
//     //     Task.FromResult(gameTurn with
//     //     {
//     //         Game = gameTurn.Game with
//     //         {
//     //             World = gameTurn.Game.World! with
//     //             {
//     //                 //WorldSettings = new WorldSettings() // TODO: Implement world settings generation
//     //             }
//     //         }
//     //     });
    
//     // public override Task<GameTurn> FinishActionAsync(GameTurn gameTurn) =>
//     //     Task.FromResult(gameTurn with
//     //     {
//     //         // GameMaster = gameTurn.GameMaster with
//     //         // {
//     //         //     Message = "You have successfully generated world settings for your game world.", // TODO: Describe them for player.
//     //         // }
//     //     });
    
//     // public override Task<GameTurn> PrepareNextTurnAsync(GameTurn gameTurn) =>
//     //     Task.FromResult(gameTurn with
//     //     {
//     //         // GameMaster = gameTurn.GameMaster with
//     //         // {
//     //         //     Question = "What would you like to do next?",
//     //         //     PossibleActions = new Collection<IGameAction>
//     //         //     {
//     //         //         // new GenerateWorldDescriptionGameAction() // TODO: Implement world description generation
//     //         //     }
//     //         // }
//     //     });
// }