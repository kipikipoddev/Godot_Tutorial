using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Movment_Model :
    State_Machine<Moving_States>,
    IMovment_Model,
    IHandler<Move_Command>
{
    private readonly IEntity_Model owner;
    private readonly ITimer_Model recharge_timer;
    private Vector2I new_position;
    public IRange_Model Movment_Charges { get; }
    public bool Can_Move => owner.Is_Alive & !owner.Is_Root();

    public Movment_Model(IEntity_Model owner, Movment_Data data)
    {
        this.owner = owner;
        Movment_Charges = new Range_Model(data.Max_Tiles, 0);
        recharge_timer = new Timer_Model(data.Recharge_Time, Recharge_Done);

        On_Enter_Deleyed(Moving_States.Moving_From, 1, () => State = Moving_States.Moving_To);
        On_Enter_Deleyed(Moving_States.Moving_To, 1, () => State = Moving_States.Can_Move);
        On_Enter(Moving_States.Moving_To, () => new Set_Position_Command(owner, new_position));

        Add_Transition(Moving_States.Can_Move, Moving_States.Cant_Move, () => !Can_Move);
        Add_Transition(Moving_States.Cant_Move, Moving_States.Can_Move, () => Can_Move);

        Mediator.Add_Handler(this, owner);
    }

    public void Handle(Move_Command cmd)
    {
        var distance = owner.Position.Get_Distance(cmd.Position);
        if (State == Moving_States.Can_Move & distance <= Movment_Charges.Amount)
            Move(cmd.Position, distance);
    }

    private void Move(Vector2I position, int distance)
    {
        new_position = position;
        State = Moving_States.Moving_From;
        new Add_Amount_Command(Movment_Charges, -distance);
        new Timer_Command(recharge_timer);
        new Move_Event(owner);
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