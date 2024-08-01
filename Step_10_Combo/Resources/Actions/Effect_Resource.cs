using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Effect_Resource : Action_Resource
{
	[Export(PropertyHint.Range, "1,10")]
	public int Effect_Time;
}
