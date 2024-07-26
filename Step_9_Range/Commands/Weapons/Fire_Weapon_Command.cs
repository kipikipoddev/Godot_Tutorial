namespace Hex_Space_Rpg.Commands;

public record Fire_Weapon_Command(IWeapon_Model Weapon, IEntity_Model Target) 
    : Command(Weapon)
{
}