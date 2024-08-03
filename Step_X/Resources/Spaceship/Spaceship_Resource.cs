using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Spaceship_Resource : Entity_Resource
{
    [Export]
    public Armor_Resource[] Armor;

    [Export]
    public Ability_Resource[] Abilities;

    public Spaceship_Data Map()
    {
        var data = new Spaceship_Data()
        {
            Armor = Armor.Select(a => a.Map()).ToArray(),
            Abilities = Abilities.Select(a => a.Map()).ToArray()
        };
        Add(data);
        return data;
    }
}
