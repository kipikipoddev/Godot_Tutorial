namespace Hex_Space_Rpg.Commands;

public record Fire_Ability_Command(IAbility_Model Model, IEntity_Model Target) : Command(Model)
{
}