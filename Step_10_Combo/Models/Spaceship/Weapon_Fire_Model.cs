using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Fire_Model : IListener<Update_Event>
{
    private readonly IWeapon_Model weapon;

    public Weapon_Fire_Model(IWeapon_Model weapon)
    {
        this.weapon = weapon;
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
            .Where(s => Get_Posible(weapon.Action, s))
            .OrderBy(Get_Order)
            .FirstOrDefault();
    }

    private bool Get_Posible(IAction_Model action, ISpaceship_Model target)
    {
        var posible = weapon.Posible(target);
        if (!posible)
            return false;
        if (action is Shield_Action_Model)
            return target.Shield.Not_Max;
        else if (action is Repair_Action_Model)
            return target.Hp.Not_Max;
        else if (action is Buff_Action_Model buff)
            return !target.Effects.OfType<Buff_Model>().Any(b => b.Name == buff.Name);
        else if (action is Aggregate_Action_Model agg)
            return Get_Posible(agg.Actions[0], target);
        return true;
    }

    private int Get_Order(ISpaceship_Model target)
    {
        if (weapon.Action is Shield_Action_Model)
            return Get_Order(target.Shield);
        else if (weapon.Action is Repair_Action_Model)
            return Get_Order(target.Hp);
        else
            return target.Position.Get_Distance(weapon.Owner.Position.Value);
    }

    private int Get_Order(IRange_Model range)
    {
        return range.Amount - range.Max;
    }
}