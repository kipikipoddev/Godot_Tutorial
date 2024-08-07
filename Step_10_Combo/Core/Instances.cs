
public static class Instances
{
    private static readonly Dictionary<Type, List<object>> instances = new();

    public static void Add<T>(T obj)
    {
        var type = typeof(T);
        if (!instances.ContainsKey(type))
            instances[type] = new();
        instances[type].Add(obj);
    }

    public static T Get<T>()
    {
        return instances.ContainsKey(typeof(T)) ?
            (T)instances[typeof(T)].FirstOrDefault() :
            Create<T>();
    }

    public static T Create<T>()
    {
        var type = Get_Type<T>();
        var obj = (T)Activator.CreateInstance(type);
        Add(obj);
        return obj;
    }

    public static IEnumerable<T> Get_All<T>()
    {
        return instances.ContainsKey(typeof(T)) ?
            instances[typeof(T)].Select(o => (T)o) :
            Array.Empty<T>();
    }

    private static Type Get_Type<T>()
    {
        return typeof(Instances).Assembly.GetTypes()
            .First(t => t.IsClass & t.IsAssignableTo(typeof(T)));
    }
}