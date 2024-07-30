namespace TestKitLibrary;

public sealed class TestKitConfiguration
{
    private readonly IList<object> myItems;

    public TestKitConfiguration()
    {
        myItems = [];
    }

    public void Add(params object[] items)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void Add<T>(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        myItems.Add(item);
    }

    public void Remove(params object[] items)
    {
        if (items is null) throw new ArgumentNullException(nameof(items));
        foreach(var item in items)
        {
            Remove(item);
        }
    }

    public bool Remove<T>(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        return myItems.Remove(item);
    }

    public bool Remove<T>()
    {
        var item = myItems.OfType<T>().FirstOrDefault();
        return item != null && Remove(item);
    }

    public void Replace<T>(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        Remove<T>();
        Add(item);
    }

    public T Get<T>()
    {
        var item = myItems.OfType<T>().FirstOrDefault();
        return item == null 
            ? throw new TestKitItemNotFoundException(typeof(T))
            : item;
    }

    public IList<T> All<T>() => myItems.OfType<T>().ToList();

    public IList<object> All() => myItems;
}
