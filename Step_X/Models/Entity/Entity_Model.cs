using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Entity_Model : IEntity_Model, IHandler<Set_Hover_Command>
{
    public string Name { get; }
    public IRange_Model Hp { get; }
    public IRange_Model Shield { get; }
    public List<IEffect_Model> Effects { get; }
    public ITeam_Model Team { get; }
    public IPosition_Model Position { get; }
    public IMovment_Model Movment { get; }
    public bool Is_Hovering { get; private set; }

    public Entity_Model(Entity_Data data, Team_Data team, Vector2I start_position)
    {
        Name = data.Name;
        Hp = new Hp_Model(data.Hp, this);
        Shield = new Shield_Model(this, data.Shield);
        Effects = new();
        Team = new Team_Model(team);
        Position = new Position_Model(this, start_position);
        Movment = new Movment_Model(this, data.Movment);
        Mediator.Add_Handler(this);
    }

    public void Handle(Set_Hover_Command cmd)
    {
        Is_Hovering = cmd.Value;
    }
}