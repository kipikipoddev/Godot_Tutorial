using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public record Over_Time_Data(string Name, Action_Data Action, int Time_Between_Occurs, int Occur_Times) : Action_Data
{
	public override IAction_Model Map(ISpaceship_Model owner)
	{
		return new Over_Time_Action_Model(this, owner);
	}
}
