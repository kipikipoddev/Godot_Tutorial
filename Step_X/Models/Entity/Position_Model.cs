using Godot;
using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Position_Model : IPosition_Model, IHandler<Set_Position_Command>
{
    private readonly IDistance_Model distance_model;
    public Vector2I Value { get; private set; }

    public Position_Model(IEntity_Model owner, Vector2I position)
    {
        Value = position;
        distance_model = Instances.Get<IDistance_Model>();
        Mediator.Add_Handler(this, owner);
    }

    public int Get_Distance(Vector2I position)
    {
        return distance_model.Get_Distance(position, Value);
    }

    public void Handle(Set_Position_Command cmd)
    {
        Value = cmd.Position;
    }
}