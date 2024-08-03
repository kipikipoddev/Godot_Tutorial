using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Over_Time_Resource : Action_Resource
{
	[Export]
	public Action_Resource Action;

	[Export(PropertyHint.Range, "1,10")]
	public int Occur_Times;

	[Export(PropertyHint.Range, "1,10")]
	public int Time_Between_Occurs;

	public override Action_Data Map(Ability_Resource ability)
	{
		return new Over_Time_Data(ability.Name, Action.Map(ability), Time_Between_Occurs, Occur_Times);
	}
}