namespace Hex_Space_Rpg.Interfaces;

public interface ISpaceship_Model : IEntity_Model
{
    IWeapon_Model[] Weapons { get; }
}