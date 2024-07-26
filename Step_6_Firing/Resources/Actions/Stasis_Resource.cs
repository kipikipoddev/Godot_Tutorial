using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Stasis_Resource : Action_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Effect_Time;

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Stasis_Data(Effect_Time);
	}
}
