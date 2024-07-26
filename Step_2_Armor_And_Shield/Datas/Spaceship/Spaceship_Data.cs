using Godot;

namespace Hex_Space_Rpg.Datas;

public class Spaceship_Data : Entity_Data
{
    public int Armor;
    public Weapon_Data[] Weapons;

    public ISpaceship_Model Map(Team_Data team, Vector2I start_position)
    {
        return Instances.Create<ISpaceship_Model>(this, team, start_position);
    }
}