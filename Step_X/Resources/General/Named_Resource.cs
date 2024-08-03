using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Named_Resource : Resource
{
	private string name;

	[Export]
	public string Name
	{
		get
		{
			if (string.IsNullOrEmpty(name))
			{
				var slash = ResourcePath.LastIndexOf('/') + 1;
				var dot = ResourcePath.LastIndexOf('.');
				name = ResourcePath.Substring(slash, dot - slash);
				name = name.Replace("_", " ");
			}
			return name;
		}
		set
		{
			name = value;
		}
	}
}
