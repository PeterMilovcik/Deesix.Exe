namespace Deesix.Tests;

public static class TestKit
{
    private static readonly object myLock = new();

    private static IList<object> myItems = null!;

    private static IList<object> Items
    {
        get
        {
            if (myItems is null)
            {    
                lock (myLock)
                {
                    if (myItems is null)
                    {
                        myItems = [];
                    }
                }
            }
            return myItems;
        }
        set 
        {
            lock (myLock)
            {
                myItems = value;
            }
        }
    }

    public static void Set(IEnumerable<object> items) 
    {
        if (items is null) throw new ArgumentNullException(nameof(items));
        Items.Clear();
        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    public static bool Has<T>() 
        where T : class => 
        Items.OfType<T>().Any();

    public static T Get<T>()
        where T : class
    {
        var item = Items.OfType<T>().FirstOrDefault();
        return item ?? throw new TestKitItemNotFoundException(typeof(T));
    }

    public static IEnumerable<object> All() => Items;
}
