
namespace Hex_Space_Rpg.Interfaces;

public interface IWeapon_Model : IName_Model
{
    ITimer_Model Cooldown { get; }
    IAction_Model[] Actions { get; }

    bool Is_Available();
    bool Posible(IEntity_Model target);
}