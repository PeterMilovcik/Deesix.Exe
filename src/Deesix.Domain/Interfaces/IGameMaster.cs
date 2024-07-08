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
