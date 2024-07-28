using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Stun_Model : Effect_Model
{
    private readonly IWeapon_Model[] weapons;

    public Stun_Model(int time, ISpaceship_Model target)
        : base("Stun", time, target)
    {
        weapons = target.Weapons;
    }

    protected override void Done()
    {
        base.Done();
        foreach (var weapon in weapons)
            if (weapon.Is_Firing)
                new Timer_Command(weapon.Firing, Timer_Action.Stop);
    }
}