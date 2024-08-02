using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Position_Model : IPosition_Model, IHandler<Move_Command>
{
    private readonly IDistance_Model distance_model;
    private readonly IEntity_Model owner;
    private readonly Range_Model movment_charges;
    private readonly Timer_Model recharge_timer;

    public Vector2I Value { get; private set; }
    public IRange_Model Movment_Charges => movment_charges;

    public Position_Model(IEntity_Model owner, Movment_Data data, Vector2I position)
    {
        this.owner = owner;
        Value = position;
        movment_charges = new Range_Model(data.Max_Tiles, 0);
        recharge_timer = new Timer_Model(data.Recharge_Time, Recharge_Done);
        distance_model = Instances.Get<IDistance_Model>();
        Mediator.Add_Handler(this);
    }

    public void Handle(Move_Command cmd)
    {
        var distance = Get_Distance(cmd.Position);
        if (Can_Move(distance))
        {
            Value = cmd.Position;
            movment_charges.Amount -= distance;
            new Timer_Command(recharge_timer).Send();
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
            movment_charges.Amount++;
            var reduction = owner.Get_Buff(Buff_Type.Movment);
            new Timer_Command(recharge_timer, Timer_Action.Start, reduction).Send();
        }
    }
}