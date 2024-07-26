namespace Hex_Space_Rpg.Commands;

public record Weapon_Fired_Command(IWeapon_Model Weapon, IEntity_Model Target) 
    : Command(Weapon)
{
}