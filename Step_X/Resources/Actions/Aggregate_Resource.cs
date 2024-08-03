using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Aggregate_Resource : Action_Resource
{
	[Export]
	public Action_Resource[] Actions;

	public override Action_Data Map(Ability_Resource ability)
	{
		var actions = Actions.Select(a => a.Map(ability)).ToArray();
		return new Aggregate_Data(actions);
	}
}