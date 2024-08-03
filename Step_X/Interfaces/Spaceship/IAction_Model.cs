
namespace Hex_Space_Rpg.Interfaces;

public interface IAction_Model
{
    bool On_Friendly { get; }

    bool Posible(IEntity_Model target);
    void Perform(IEntity_Model target);
}