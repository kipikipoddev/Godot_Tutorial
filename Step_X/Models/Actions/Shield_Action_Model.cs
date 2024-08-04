using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Shield_Action_Model : Action_Model
{
    private readonly int amount;

    public Shield_Action_Model(Shield_Data data, IAbility_Model owner)
        : base(owner)
    {
        amount = data.Amount;
    }

    public override void Perform(IEntity_Model target)
    {
        var buffed_amount = Owner.Owner.Get_Buffed(Buff_Type.Shield, amount);
        new Add_Amount_Command(target.Shield, buffed_amount).Send();
    }
}