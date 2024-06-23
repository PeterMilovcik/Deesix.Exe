namespace Deesix.Core.Exceptions;

[Serializable]
public class OpenAIApiKeyNotFoundException : Exception
{
    public OpenAIApiKeyNotFoundException() : this("OpenAI API key not found.")
    {
    }

    public OpenAIApiKeyNotFoundException(string? message) : base(message)
    {
    }

    public OpenAIApiKeyNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}