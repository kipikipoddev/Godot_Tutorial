using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Damage_Action_Model : Action_Model
{
    private readonly int amount;

    public Damage_Action_Model(Damage_Data data, IWeapon_Model owner)
        : base(owner)
    {
        On_Friendly = false;
        amount = data.Amount;
    }

    public override void Perform(IEntity_Model target)
    {
        new Damage_Command(target, amount, Owner.Type).Send();
    }
}