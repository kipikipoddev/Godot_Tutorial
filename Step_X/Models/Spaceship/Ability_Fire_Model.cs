using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Ability_Fire_Model : IListener<Update_Event>
{
    private readonly IAbility_Model ability;

    public Ability_Fire_Model(IAbility_Model ability)
    {
        this.ability = ability;
        Mediator.Add_Listener(this);
    }

    public void Handle(Update_Event evnt)
    {
        if (ability.In_Cooldown)
            return;

        var target = Get_Target();
        if (target != null)
            new Fire_Ability_Command(ability, target).Send();
    }

    private ISpaceship_Model Get_Target()
    {
        return Instances.Get_All<ISpaceship_Model>()
            .Where(s => Get_Posible(ability.Action, s))
            .OrderBy(Get_Order)
            .FirstOrDefault();
    }

    private bool Get_Posible(IAction_Model action, ISpaceship_Model target)
    {
        var posible = ability.Posible(target);
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
        if (ability.Action is Shield_Action_Model)
            return Get_Range_Order(target.Shield);
        else if (ability.Action is Repair_Action_Model)
            return Get_Range_Order(target.Hp);
        else
            return target.Position.Get_Distance(ability.Owner.Position.Value);
    }

    private int Get_Range_Order(IRange_Model range)
    {
        return range.Amount - range.Max;
    }
}