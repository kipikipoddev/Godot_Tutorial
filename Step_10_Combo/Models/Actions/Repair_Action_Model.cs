using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Repair_Action_Model : Action_Model
{
    private readonly int amount;

    public Repair_Action_Model(Repair_Data data, IWeapon_Model owner)
        : base(owner)
    {
        amount = data.Amount;
    }

    public override void Perform(IEntity_Model target)
    {
        var buffed_amount = Owner.Owner.Get_Buffed(Buff_Type.Repair, amount);
        new Heal_Command(target, buffed_amount).Send();
    }
}