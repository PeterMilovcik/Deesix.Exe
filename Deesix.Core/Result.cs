namespace Deesix.AI.Core;

public record Result<T>(bool Success, T? Value, string ErrorMessage)
{
    public void Deconstruct(out bool success, out T? data, out string errorMessage)
    {
        success = Success;
        data = Value;
        errorMessage = ErrorMessage;
    }

    public Result<T> OnSuccess(Action<T> action)
    {
        if (Success) action(Value!);
        return this;
    }

    public Result<T> OnFailure(Action<string> action)
    {
        if (!Success) action(ErrorMessage);
        return this;
    }
}
