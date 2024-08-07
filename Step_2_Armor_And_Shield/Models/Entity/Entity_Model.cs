﻿using Godot;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Entity_Model : IEntity_Model
{
    public string Name { get; }
    public IRange_Model Hp { get; }
    public ITeam_Model Team { get; }
    public IPosition_Model Position { get; }

    public Entity_Model(Entity_Data data, Team_Data team, Vector2I start_position)
    {
        Name = data.Name;
        Hp = new Hp_Model(data.Hp, this);
        Team = new Team_Model(team);
        Position = new Position_Model(start_position);
    }
}