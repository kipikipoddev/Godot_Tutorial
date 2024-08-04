namespace Hex_Space_Rpg.Commands;

public record Add_Amount_Command(IAmount_Model Model, int Amount) : Command(Model)
{
}