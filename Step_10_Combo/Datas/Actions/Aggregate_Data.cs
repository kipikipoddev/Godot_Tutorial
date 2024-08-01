using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Aggregate_Data(Action_Data[] Actions) : Action_Data
{
	public override IAction_Model Map(IWeapon_Model owner)
	{
		return new Aggregate_Action_Model(this, owner);
	}
}
