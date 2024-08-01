using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Stun_Resource : Effect_Resource
{
	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Stun_Data(Effect_Time);
	}
}
