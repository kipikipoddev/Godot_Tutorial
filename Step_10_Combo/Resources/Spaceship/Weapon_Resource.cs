using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Weapon_Resource : Named_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Cooldown_Time;

	[Export(PropertyHint.Range, "1,10")]
	public int Range;

	[Export]
	public Type_Resource Type;

	[Export]
	public Action_Resource Action;

	public Weapon_Data Map()
	{
		return new Weapon_Data()
		{
			Name = Name,
			Cooldown_Time = Cooldown_Time,
			Type = Type.Map(),
			Range = Range,
			Action = Action.Map(this)
		};
	}
}
