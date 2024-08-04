namespace Hex_Space_Rpg.Commands;

public record Set_Hover_Command(IEntity_Model Model, bool Value) : Command(Model)
{
}