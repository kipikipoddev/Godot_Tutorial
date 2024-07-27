using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Spaceship_Resource : Entity_Resource
{
    [Export]
    public Armor_Resource[] Armor;

    [Export]
    public Weapon_Resource[] Weapons;

    [Export]
    public int Max_Shield;

    [Export]
    public int Movment_Cooldown;

    public Spaceship_Data Map()
    {
        return new Spaceship_Data()
        {
            Name = Name,
            Hp = Hp,
            Max_Shield = Max_Shield,
            Armor = Armor.Select(a => a.Map()).ToArray(),
            Weapons = Weapons.Select(a => a.Map()).ToArray(),
            Movment_Cooldown = Movment_Cooldown
        };
    }
}
