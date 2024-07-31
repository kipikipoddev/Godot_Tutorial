using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Stasis_Action_Model : Action_Model
{
    private readonly int time;

    public Stasis_Action_Model(Stasis_Data data, IWeapon_Model owner)
        : base(owner)
    {
        time = data.Effect_Time;
        On_Friendly = false;
    }

    public override void Perform(ISpaceship_Model target)
    {
        while (target.Effects.Any())
            (target.Effects.First() as Effect_Model).Remove();
        new Stasis_Model(time, target);
        foreach (var weapon in target.Weapons)
            (weapon.Cooldown as Timer_Model).Pause();
    }
}