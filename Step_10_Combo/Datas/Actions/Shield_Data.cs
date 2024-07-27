using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Shield_Data(int Amount) : Action_Data
{
	public override IAction_Model Map(IWeapon_Model owner)
	{
		return new Shield_Action_Model(this, owner);
	}
}
