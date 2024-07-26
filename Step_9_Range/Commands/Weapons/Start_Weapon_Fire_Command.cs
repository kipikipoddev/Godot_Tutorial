namespace Hex_Space_Rpg.Commands;

public record Start_Weapon_Fire_Command(
        ISpaceship_Model Spaceship,
        int Time,
        IWeapon_Model Weapon,
        IEntity_Model Target)
    : Command(Spaceship)
{
}