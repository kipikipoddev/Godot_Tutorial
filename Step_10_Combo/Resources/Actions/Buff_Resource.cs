using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Buff_Resource : Effect_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Amount;

	[Export(PropertyHint.Enum)]
	public Buff_Type Type;

	public override Action_Data Map(Weapon_Resource weapon)
	{
		return new Buff_Data(Effect_Time, Amount, Type);
	}
}
