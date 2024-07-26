namespace Hex_Space_Rpg.Core;

record Listener_Data(Type Event_Type, object Listener, Action<object> Listen) { }
record Handler_Data(Type Command_Type, object Handler, object Model, Action<object> Handle) { }