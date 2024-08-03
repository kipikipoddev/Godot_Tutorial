using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Root_Resource : Effect_Resource
{
	public override Action_Data Map(Ability_Resource ability)
	{
		return new Root_Data(Effect_Time);
	}
}
