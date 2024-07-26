using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Entity_Model : IEntity_Model, IListener<Update_Event>
{
    public string Name { get; }
    public IRange_Model Hp { get; }
    public List<IEffect_Model> Effects { get; }
    public ITeam_Model Team { get; }
    public IPosition_Model Position { get; }

    public Entity_Model(Entity_Data data, Team_Data team, Vector2I start_position)
    {
        Name = data.Name;
        Hp = new Hp_Model(data.Hp, this);
        Effects = new();
        Team = new Team_Model(team);
        Position = new Position_Model(start_position);

        Mediator.Add_Listener(this);
    }

    public void Handle(Update_Event evnt)
    {
        if (Hp.Is_Min)
            while (Effects.Any())
                new Remove_Effect_Command(Effects[0]).Send();
    }
}