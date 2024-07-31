namespace Hex_Space_Rpg.Models;

public class Stasis_Model : Stun_Model
{
    public Stasis_Model(int time, ISpaceship_Model target)
        : base(time, target)
    {
        Name = "Stasis";
    }

    protected override void Done()
    {
        base.Done();
        foreach (var weapon in (Target as ISpaceship_Model).Weapons)
            (weapon.Cooldown as Timer_Model).Resume();
    }
}