using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Repair_Data(int Amount) : Action_Data
{
	public override IAction_Model Map(IAbility_Model owner)
	{
		return new Repair_Action_Model(this, owner);
	}
}
