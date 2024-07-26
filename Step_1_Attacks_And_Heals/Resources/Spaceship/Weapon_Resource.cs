using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Weapon_Resource : Named_Resource
{
	[Export]
	public Action_Resource[] Actions;

	public Weapon_Data Map()
	{
		return new Weapon_Data()
		{
			Name = Name,
			Actions = Actions.Select(a => a.Map(this)).ToArray()
		};
	}
}
