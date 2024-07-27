using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Combo_Data(Action_Data[] Actions) : Action_Data
{
	public override IAction_Model Map(IWeapon_Model owner)
	{
		return new Combo_Action_Model(this, owner);
	}
}
