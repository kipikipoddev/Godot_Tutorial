using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Revive_Resource : Action_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int To_Hp;

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Revive_Data(To_Hp);
	}
}
