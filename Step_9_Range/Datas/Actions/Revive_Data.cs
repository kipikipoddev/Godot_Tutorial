using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Revive_Data(int To_Hp) : Action_Data
{
    public override IAction_Model Map(ISpaceship_Model owner)
    {
        return new Revive_Action_Model(this, owner);
    }
}
