namespace Hex_Space_Rpg.Commands;

public record Set_Shield_Command(ISpaceship_Model Spaceship, int Amount) : Command(Spaceship)
{
}