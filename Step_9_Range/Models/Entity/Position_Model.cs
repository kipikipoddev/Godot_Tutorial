using Godot;
using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Position_Model : IPosition_Model, IHandler<Move_Command>
{
    private readonly IDistance_Model distance_model;

    public Vector2I Value { get; private set; }
    public ITimer_Model Movment_Cooldown { get; }


    public Position_Model(Vector2I position, int cooldown)
    {
        Value = position;
        Movment_Cooldown = new Timer_Model(cooldown);
        distance_model = Instances.Get<IDistance_Model>();
        Mediator.Add_Handler(this);
    }

    public void Handle(Move_Command cmd)
    {
        if (cmd.Is_Valid)
        {
            Value = cmd.Position;
            new Timer_Command(Movment_Cooldown).Send();
        }
    }

    public int Get_Distance(Vector2I position)
    {
        return distance_model.Get_Distance(position, Value);
    }
}