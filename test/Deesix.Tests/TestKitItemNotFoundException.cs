namespace Deesix.Tests;

[Serializable]
internal class TestKitItemNotFoundException : Exception
{
    private Type type;

    public TestKitItemNotFoundException()
    {
    }

    public TestKitItemNotFoundException(Type type)
    {
        this.type = type;
    }

    public TestKitItemNotFoundException(string? message) : base(message)
    {
    }

    public TestKitItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}