﻿using Godot;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Spaceship_Model : Entity_Model, ISpaceship_Model
{
    public IArmor_Model[] Armor { get; }
    public IRange_Model Shield { get; }
    public IWeapon_Model[] Weapons { get; }

    public Spaceship_Model(Spaceship_Data data, Team_Data team, Vector2I start_position)
        : base(data, team, start_position)
    {
        Armor = data.Armor.Select(data => new Armor_Model(data, this)).ToArray();
        Shield = new Shield_Model(this, data.Max_Shield);
        Weapons = data.Weapons.Select(a => a.Map(this)).ToArray();
        new Movment_Model(this);

        Instances.Add<ISpaceship_Model>(this);
    }
}