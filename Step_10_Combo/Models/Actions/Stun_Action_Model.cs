using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Stun_Action_Model : Action_Model
{
    private readonly int time;
    public Stun_Action_Model(Stun_Data data, IWeapon_Model owner)
        : base(owner)
    {
        On_Friendly = false;
        time = data.Effect_Time;
    }

    public override void Perform(IEntity_Model target)
    {
        new Stun_Model(time, target);
    }
}