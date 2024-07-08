namespace Deesix.Domain.Interfaces;

public class GameOptionResult
{
    public string Message { get; }

    public GameOptionResult(string resultMessage)
    {
        Message = resultMessage;
    }
}
