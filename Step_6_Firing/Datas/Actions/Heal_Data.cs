using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Heal_Data(int Amount) : Action_Data
{
	public override IAction_Model Map(ISpaceship_Model owner)
	{
		return new Heal_Action_Model(this, owner);
	}
}
