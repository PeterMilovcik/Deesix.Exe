using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Domain.Interfaces;

public interface IGameMaster
{
    Maybe<Game> Game { get; }
    string GetMessage();
    string GetQuestion();
    IGameOption[] GetOptions();
    Task ProcessOptionAsync(IGameOption option);
}

public class GameOptionResult
{
    public string ResultMessage { get; }

    public GameOptionResult(string resultMessage)
    {
        ResultMessage = resultMessage;
    }
}

public interface IGameOption
{
    string Description { get; }

    bool CanExecute(Maybe<Game> game);
}