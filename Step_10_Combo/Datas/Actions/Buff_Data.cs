using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public enum Buff_Type
{
	Damage,
	Repair,
	Movment,
	Cooldown
}

public record Buff_Data(int Effect_Time, int Amount, Buff_Type Type) : Action_Data
{
	public override IAction_Model Map(IWeapon_Model owner)
	{
		return new Buff_Action_Model(this, owner);
	}
}
