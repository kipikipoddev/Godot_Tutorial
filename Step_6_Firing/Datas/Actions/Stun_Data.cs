using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Stun_Data(int Effect_Time) : Action_Data
{
    public override IAction_Model Map(ISpaceship_Model owner)
    {
        return new Stun_Action_Model(this, owner);
    }
}
