using System.ComponentModel;

namespace TestKitLibrary;

public class SUT : IRevertibleChangeTracking
{
    public bool IsChanged => throw new NotImplementedException();

    public void AcceptChanges()
    {
        throw new NotImplementedException();
    }

    public void RejectChanges()
    {
        throw new NotImplementedException();
    }
}
