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

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Over_Time_Data(weapon.Name, Action.Map(weapon), Time_Between_Occurs, Occur_Times);
	}
}