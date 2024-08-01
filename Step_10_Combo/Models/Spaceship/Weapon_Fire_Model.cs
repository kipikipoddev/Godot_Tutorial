using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Fire_Model : IListener<Update_Event>
{
    private readonly IWeapon_Model weapon;
    public bool Auto_Fire_Mode { get; private set; }

    public Weapon_Fire_Model(IWeapon_Model weapon, bool auto_fire)
    {
        this.weapon = weapon;
        Auto_Fire_Mode = auto_fire;
        if (auto_fire)
            Mediator.Add_Listener(this);
    }

    public void Handle(Update_Event evnt)
    {
        if (weapon.In_Cooldown)
            return;

        var target = Get_Target();
        if (target != null)
            new Fire_Weapon_Command(weapon, target).Send();
    }

    private ISpaceship_Model Get_Target()
    {
        return Instances.Get_All<ISpaceship_Model>()
            .Where(s => weapon.Posible(s))
            .OrderBy(Get_Order)
            .FirstOrDefault();
    }

    private int Get_Order(ISpaceship_Model target)
    {
        if (weapon.Action is Shield_Action_Model)
            return Get_Order(target.Shield);
        else if (weapon.Action is Heal_Action_Model)
            return Get_Order(target.Hp);
        else
            return target.Position.Get_Distance(weapon.Owner.Position.Value);
    }

    private int Get_Order(IRange_Model range)
    {
        return range.Amount - range.Max;
    }
}