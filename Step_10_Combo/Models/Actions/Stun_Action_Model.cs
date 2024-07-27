using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Stun_Action_Model : Action_Model
{
    private readonly int time;
    private readonly int damage;

    public Stun_Action_Model(Stun_Data data, IWeapon_Model owner)
        : base(owner)
    {
        On_Friendly = false;
        time = data.Effect_Time;
        damage = data.Damage;
    }

    public override void Perform(ISpaceship_Model target)
    {
        new Damage_Command(target, damage, Owner.Type).Send();
        new Stun_Model(time, target);
    }
}