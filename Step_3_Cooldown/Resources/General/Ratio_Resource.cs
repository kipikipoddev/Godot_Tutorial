using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Ratio_Resource : Resource
{
	[Export]
	public Color Color;

	[Export]
	public float Ratio;
}
