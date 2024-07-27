namespace Hex_Space_Rpg.Commands;

public record Heal_Command(IEntity_Model Entity, int Amount) : Command(Entity), IAmount_Model
{
}