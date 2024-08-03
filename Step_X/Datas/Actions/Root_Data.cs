using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Root_Data(int Effect_Time) : Action_Data
{
    public override IAction_Model Map(IAbility_Model owner)
    {
        return new Root_Action_Model(this, owner);
    }
}