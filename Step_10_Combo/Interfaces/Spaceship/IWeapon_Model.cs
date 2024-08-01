using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Interfaces;

public interface IWeapon_Model : IName_Model, ITyped
{
    int Range { get; }
    ISpaceship_Model Owner { get; }
    ITimer_Model Cooldown { get; }
    IAction_Model Action { get; }
    bool Is_Auto_Fire { get; }

    bool In_Cooldown => Cooldown.Running;
    bool Cant_Shoot => Owner.Is_Stun();
    bool Ideal => !In_Cooldown & !Cant_Shoot;

    bool Posible(IEntity_Model target);
}