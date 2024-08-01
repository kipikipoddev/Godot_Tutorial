using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Shield_Action_Model : Action_Model
{
    private readonly int amount;

    public Shield_Action_Model(Shield_Data data, IWeapon_Model owner)
        : base(owner)
    {
        amount = data.Amount;
    }

    public override void Perform(IEntity_Model target)
    {
        new Add_Shield_Command(target, amount).Send();
    }
}