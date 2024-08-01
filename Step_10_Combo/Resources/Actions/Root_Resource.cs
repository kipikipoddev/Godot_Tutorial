using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Root_Resource : Action_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Efect_Time;

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Root_Data(Efect_Time);
	}
}
