namespace Hex_Space_Rpg.Core;

public interface IListener<TEvent>
    where TEvent : Event
{
    void Handle(TEvent evnt);
}
