using Godot;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Spaceship_Model : Entity_Model, ISpaceship_Model
{
    public IWeapon_Model[] Weapons { get; }

    public Spaceship_Model(Spaceship_Data data, Team_Data team, Vector2I start_position)
        : base(data, team, start_position)
    {
        Weapons = data.Weapons.Select(a => a.Map(this)).ToArray();

        Instances.Add(this);
    }
}