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

    public override void Perform(IEntity_Model target)
    {
        foreach (var effect in target.Effects)
            new Timer_Command(effect.Timer, Timer_Action.Pause).Send();
        new Stasis_Model(time, target);
        if (target is ISpaceship_Model ship)
            foreach (var weapon in ship.Weapons)
                new Timer_Command(weapon.Cooldown, Timer_Action.Pause).Send();
    }
}