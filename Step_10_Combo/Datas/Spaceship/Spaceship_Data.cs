using Godot;
using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public class Spaceship_Data : Entity_Data
{
    public Armor_Data[] Armor;
    public Weapon_Data[] Weapons;

    public ISpaceship_Model Map(Team_Data team, Vector2I start_position)
    {
        return new Spaceship_Model(this, team, start_position);
    }
}