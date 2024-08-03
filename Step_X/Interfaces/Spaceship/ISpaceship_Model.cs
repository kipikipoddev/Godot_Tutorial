namespace Hex_Space_Rpg.Interfaces;

public interface ISpaceship_Model : IEntity_Model
{
    IArmor_Model[] Armor { get; }
    IAbility_Model[] Abilities { get; }
}