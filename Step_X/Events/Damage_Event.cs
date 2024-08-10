namespace Hex_Space_Rpg.Events;

public record Damage_Event(IEntity_Model Model, int Amount, bool Is_Shield) : Event
{
}