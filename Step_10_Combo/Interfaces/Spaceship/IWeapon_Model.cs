
namespace Hex_Space_Rpg.Interfaces;

public interface IWeapon_Model : IName_Model, ITyped
{
    int Range { get; }
    ISpaceship_Model Owner { get; }
    ITimer_Model Cooldown { get; }
    IAction_Model Action { get; }

    bool Is_Cooldown => Cooldown.Running;

    bool Posible();
    bool Posible(IEntity_Model target);
}