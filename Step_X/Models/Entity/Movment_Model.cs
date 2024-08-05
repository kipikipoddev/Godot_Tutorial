using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Movment_Model : IMovment_Model, IHandler<Move_Command>
{
    private readonly IEntity_Model owner;
    private readonly ITimer_Model recharge_timer;
    private readonly ITimer_Model movment_timer;
    private Vector2I new_position;
    public IRange_Model Movment_Charges { get; }
    public bool Is_Moving { get; private set; }

    public Movment_Model(IEntity_Model owner, Movment_Data data)
    {
        this.owner = owner;
        Movment_Charges = new Range_Model(data.Max_Tiles, 0);
        movment_timer = new Timer_Model(1, Move_Done, false);
        recharge_timer = new Timer_Model(data.Recharge_Time, Recharge_Done);
        Mediator.Add_Handler(this, owner);
    }

    public void Handle(Move_Command cmd)
    {
        var distance = owner.Position.Get_Distance(cmd.Position);
        if (Can_Move(distance))
        {
            new_position = cmd.Position;
            Is_Moving = true;
            new Add_Amount_Command(Movment_Charges, -distance);
            new Timer_Command(movment_timer);
            new Timer_Command(recharge_timer);
            new Move_Event(owner);
        }
    }

    private void Move_Done()
    {
        if (owner.Position.Value != new_position)
        {
            new Set_Position_Command(owner, new_position);
            new Timer_Command(movment_timer);
        }
        else
            Is_Moving = false;
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
            new Add_Amount_Command(Movment_Charges, 1);
            var interval = owner.Get_Buffed(Buff_Type.Movment, recharge_timer.Interval);
            new Timer_Command(recharge_timer, Timer_Action.Start, interval);
        }
    }
}