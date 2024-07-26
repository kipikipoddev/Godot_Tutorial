using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Spaceship_Resource : Entity_Resource
{
    [Export]
    public Weapon_Resource[] Weapons;

    public Spaceship_Data Map()
    {
        return new Spaceship_Data()
        {
            Name = Name,
            Hp = Hp,
            Weapons = Weapons.Select(a => a.Map()).ToArray(),
        };
    }
}
