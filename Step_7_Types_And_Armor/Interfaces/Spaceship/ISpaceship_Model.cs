namespace Hex_Space_Rpg.Interfaces;

public interface ISpaceship_Model : IEntity_Model
{
    Armor_Model[] Armor { get; }
    IRange_Model Shield { get; }
    IWeapon_Model[] Weapons { get; }
    ITimer_Model Fire { get; }

    bool Is_Firing => Fire.Running;
}