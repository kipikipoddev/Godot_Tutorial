
namespace Hex_Space_Rpg.Interfaces;

public interface IWeapon_Model : IName_Model
{
    IType_Model Type { get; }
    ITimer_Model Cooldown { get; }
    IAction_Model[] Actions { get; }
    int Range { get; }

    bool Is_Available(bool ignore_firing = false);
    bool Posible(IEntity_Model target, bool ignore_firing = false);
}