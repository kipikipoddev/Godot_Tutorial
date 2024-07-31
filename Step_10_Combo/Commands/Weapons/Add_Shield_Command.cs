namespace Hex_Space_Rpg.Commands;

public record Add_Shield_Command(ISpaceship_Model Spaceship, int Amount) : Command(Spaceship)
{
}