
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

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

    public static T Get<T>(params object[] args)
    {
        return instances.ContainsKey(typeof(T)) ?
            (T)instances[typeof(T)].FirstOrDefault() :
            Create<T>(args);
    }

    public static T Create<T>(params object[] args)
    {
        var type = Get_Type<T>();
        var obj = (T)Activator.CreateInstance(type, args);
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
        var type = typeof(T);
        if (type.IsClass)
            return type;
        return typeof(Instances).Assembly.GetTypes()
            .First(t => t.IsClass & t.IsAssignableTo(type));
    }
}