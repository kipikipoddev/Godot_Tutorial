using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Root_Action_Model : Action_Model
{
    private readonly int time;
    private readonly int damage;

    public Root_Action_Model(Root_Data data, IWeapon_Model owner)
        : base(owner)
    {
        On_Friendly = false;
        time = data.Effect_Time;
        damage = data.Damage;
    }

    public override void Perform(ISpaceship_Model target)
    {
        if (damage > 0)
            new Damage_Command(target, damage, Owner.Type).Send();
        new Root_Model(time, target);
    }
}