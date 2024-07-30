namespace TestKitLibrary;

[Serializable]
internal class TestKitItemNotFoundException : Exception
{
    public TestKitItemNotFoundException(Type type) :
        base($"Test Kit Item of type '{type}' is not found.")
    {
        Type = type;
    }

    public TestKitItemNotFoundException(Type type, Exception? innerException) : 
        base($"Test Kit Item of type '{type}' is not found.", innerException)
    {
        Type = type;
    }

    public Type Type { get; }
}