using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Damage_Resource : Action_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Amount;

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Damage_Data(Amount, weapon.Type.Map());
	}
}
