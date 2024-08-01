using System.Diagnostics;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Buff_Action_Model : Action_Model
{
    private readonly int time;
    private readonly int amount;
    private readonly Buff_Type type;

    public Buff_Action_Model(Buff_Data data, IWeapon_Model owner)
        : base(owner)
    {
        time = data.Effect_Time;
        amount = data.Amount;
        type = data.Type;
    }

    public override void Perform(IEntity_Model target)
    {
        new Buff_Model(time, amount, type, target);
    }
}