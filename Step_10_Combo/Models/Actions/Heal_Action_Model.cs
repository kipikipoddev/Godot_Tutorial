using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Heal_Action_Model : Action_Model
{
    private readonly int amount;

    public Heal_Action_Model(Heal_Data data, IWeapon_Model owner)
        : base(owner)
    {
        amount = data.Amount;
    }

    public override void Perform(ISpaceship_Model target)
    {
        new Heal_Command(target, amount).Send();
    }

    public override bool Posible(IEntity_Model target)
    {
        return base.Posible(target) & target.Hp.Not_Max;
    }
}