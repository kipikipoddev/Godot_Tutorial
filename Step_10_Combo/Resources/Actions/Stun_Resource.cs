using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Stun_Resource : Action_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Efect_Time;

	[Export(PropertyHint.Range, "0,10")]
	public int Damage;

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Stun_Data(Efect_Time, Damage);
	}
}
