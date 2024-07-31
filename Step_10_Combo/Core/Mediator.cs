namespace Hex_Space_Rpg.Core;

public static class Mediator
{
    private static readonly List<Listener_Data> listeners = new();
    private static readonly List<Handler_Data> handlers = new();

    public static void Add_Listener<TEvent>(IListener<TEvent> listener)
        where TEvent : Event
    {
        var data = new Listener_Data(typeof(TEvent), listener, e => listener.Handle((TEvent)e));
        listeners.Add(data);
    }

    public static void Add_Handler<TCommand>(IHandler<TCommand> handler, object model = null)
        where TCommand : Command
    {
        var data = new Handler_Data(typeof(TCommand), handler, model ?? handler, e => handler.Handle((TCommand)e));
        handlers.Add(data);
    }

    public static void Remove_Listener(object listener)
    {
        for (int i = 0; i < listeners.Count; i++)
            if (listeners[i].Listener == listener)
                listeners.RemoveAt(i);
    }

    public static void Send(Event evnt)
    {
        var listeners_action = listeners.Where(h => h.Event_Type == evnt.GetType())
            .Select(h => h.Listen).ToArray();
        foreach (var listener in listeners_action)
            listener(evnt);
    }

    public static void Invoke(Command cmd)
    {
        var handlers_action = handlers.Where(h => h.Command_Type == cmd.GetType() && h.Model == cmd.Model_Object)
            .Select(h => h.Handle).Reverse().ToArray();
        foreach (var handler in handlers_action)
                handler(cmd);
    }
}