namespace Hex_Space_Rpg.Interfaces;

public interface ISpaceship_Model : IEntity_Model
{
    IArmor_Model Armor { get; }
    IRange_Model Shield { get; }
    IWeapon_Model[] Weapons { get; }
}