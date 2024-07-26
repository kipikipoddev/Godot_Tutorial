using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Stasis_Data(int Effect_Time) : Action_Data
{
    public override IAction_Model Map(ISpaceship_Model owner)
    {
        return new Stasis_Action_Model(this, owner);
    }
}
