namespace Hex_Space_Rpg.Core;

record Listener_Data(Type Event_Type, object Listener, Action<object> Listen) { }

record Handler_Data(Type Command_Type, object Handler, object Model, Action<object> Handle) { }

record State_Machine_Data<TStats>(TStats From, TStats To, Func<bool> Condition) { }