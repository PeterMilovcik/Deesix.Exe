﻿using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.GameOptions;

public class LoadGamesOption(IRepository<Game> gameRepository) : IGameOption
{
    private readonly IRepository<Game> gameRepository = gameRepository
        ?? throw new ArgumentNullException(nameof(gameRepository));

    public string Title => "Load Game";

    public int Order => 2;

    public bool CanExecute(GameTurn gameTurn) => gameTurn.Game.HasNoValue && gameRepository.GetAll().Any();

    public Task<GameTurn> ExecuteAsync(GameTurn gameTurn)
    {
        var games = gameRepository.GetAll().ToList();
        var loadGameOptions = new List<IGameOption>();
        games.ForEach(game => loadGameOptions.Add(new LoadGameOption(game)));
        GameOptionResult result = new GameOptionResult("Please choose a game to play.");
        result.NextQuestion = "Which game would you like to play?";
        result.NextAdditionalGameOptions.AddRange(loadGameOptions);
        return Task.FromResult(gameTurn with 
        {
            Message = "Please choose a game to play.",
            Question = "Which one would you like to play?",
            GameOptions = new List<IGameOption>(loadGameOptions)
        });
    }
}
