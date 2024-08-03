using Godot;

namespace Hex_Space_Rpg.Datas;

public class Spaceship_Data : Entity_Data
{
    public Armor_Data[] Armor;
    public Ability_Data[] Abilities;

    public ISpaceship_Model Map(Team_Data team, Vector2I start_position)
    {
        return Instances.Create<ISpaceship_Model>(this, team, start_position);
    }
}