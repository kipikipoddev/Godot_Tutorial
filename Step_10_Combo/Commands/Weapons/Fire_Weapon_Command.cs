namespace Hex_Space_Rpg.Commands;

public record Fire_Weapon_Command(IWeapon_Model Model, IEntity_Model Target) : Command(Model)
{
}