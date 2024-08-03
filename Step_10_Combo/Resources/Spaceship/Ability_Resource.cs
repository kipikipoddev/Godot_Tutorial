using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Ability_Resource : Named_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Cooldown_Time = 1;

	[Export(PropertyHint.Range, "1,10")]
	public int Range = 1;

	[Export]
	public Type_Resource Type;

	[Export]
	public Action_Resource Action;

	[Export]
	public Action_Resource Self_Action;

	public Ability_Data Map()
	{
		return new Ability_Data()
		{
			Name = Name,
			Cooldown_Time = Cooldown_Time,
			Type = Type.Map(),
			Range = Range,
			Action = Action.Map(this),
			Self_Action = Self_Action?.Map(this)
		};
	}
}
