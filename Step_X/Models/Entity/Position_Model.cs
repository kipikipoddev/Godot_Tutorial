using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Position_Model : IPosition_Model, IHandler<Move_Command>
{
    private readonly IDistance_Model distance_model;
    private readonly IEntity_Model owner;
    private readonly IRange_Model movment_charges;
    private readonly ITimer_Model recharge_timer;
    private readonly ITimer_Model movment_timer;
    private Vector2I new_position;

    public Vector2I Value { get; private set; }
    public IRange_Model Movment_Charges => movment_charges;
    public bool Is_Moving { get; private set; }

    public Position_Model(IEntity_Model owner, Movment_Data data, Vector2I position)
    {
        this.owner = owner;
        Value = position;
        movment_charges = new Range_Model(data.Max_Tiles, 0);
        movment_timer = new Timer_Model(1, Move_Done, false);
        recharge_timer = new Timer_Model(data.Recharge_Time, Recharge_Done);
        distance_model = Instances.Get<IDistance_Model>();
        Mediator.Add_Handler(this);
    }

    private void Move_Done()
    {
        if (Value != new_position)
        {
            Value = new_position;
            new Timer_Command(movment_timer).Send();
        }
        else
            Is_Moving = false;
    }

    public void Handle(Move_Command cmd)
    {
        var distance = Get_Distance(cmd.Position);
        if (Can_Move(distance))
        {
            new_position = cmd.Position;
            Is_Moving = true;
            new Add_Amount_Command(movment_charges, -distance).Send();
            new Timer_Command(movment_timer).Send();
            new Timer_Command(recharge_timer).Send();
            new Move_Event(owner);
        }
    }

    public int Get_Distance(Vector2I position)
    {
        return distance_model.Get_Distance(position, Value);
    }

    private bool Can_Move(int distance)
    {
        if (!owner.Is_Alive | owner.Is_Root())
            return false;
        return distance <= Movment_Charges.Amount;
    }

    private void Recharge_Done()
    {
        if (Movment_Charges.Not_Max)
        {
            new Add_Amount_Command(movment_charges, 1).Send();
            var interval = owner.Get_Buffed(Buff_Type.Movment, recharge_timer.Interval);
            new Timer_Command(recharge_timer, Timer_Action.Start, interval).Send();
        }
    }
}