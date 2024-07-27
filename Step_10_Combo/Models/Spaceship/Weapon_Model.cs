using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Model : IWeapon_Model, IListener<Update_Event>, IHandler<Fire_Weapon_Command>
{
    private IEntity_Model target;

    public string Name { get; }
    public int Range { get; }

    public ISpaceship_Model Owner { get; }
    public ITimer_Model Cooldown { get; }
    public IType_Model Type { get; }
    public IAction_Model Action { get; }
    public ITimer_Model Firing { get; }

    public Weapon_Model(Weapon_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        Owner = owner;
        Action = data.Action.Map(this);
        Cooldown = new Timer_Model(data.Cooldown_Time);
        Type = new Type_Model(data.Type);
        Range = data.Range;
        Firing = new Timer_Model(data.Fire_Time);

        Mediator.Add_Handler(this);
        Mediator.Add_Listener(this);
    }

    public bool Posible()
    {
        return Posible(false);
    }

    public bool Posible(IEntity_Model target)
    {
        return Posible(target, false);
    }

    public void Handle(Update_Event evnt)
    {
        if (!Owner.Is_Alive & Cooldown.Running)
            new Timer_Command(Cooldown, Timer_Action.Stop).Send();
        else if (Firing.Running && !Posible(target, true))
            new Timer_Command(Firing, Timer_Action.Stop).Send();
        else if (Firing.Done & target != null)
        {
            Action.Perform(target);
            target = null;
            new Timer_Command(Cooldown).Send();
        }
    }

    public void Handle(Fire_Weapon_Command cmd)
    {
        if (Posible(cmd.Target, false))
        {
            new Timer_Command(Firing).Send();
            target = cmd.Target;
        }
    }

    private bool Posible(bool ignore_firing)
    {
        if (!Owner.Is_Alive | !Cooldown.Done)
            return false;
        if (!ignore_firing & Firing.Running)
            return false;
        return !Owner.Effects.OfType<Stun_Model>().Any();
    }

    private bool Posible(IEntity_Model target, bool ignore_firing)
    {
        if (target == null || !Posible(ignore_firing))
            return false;

        if (Owner.Effects.OfType<Stun_Model>().Any())
            return false;

        if (target.Effects.OfType<Stasis_Model>().Any())
            return false;

        var distance = Owner.Position.Get_Distance(target.Position.Value);
        if (distance > Range)
            return false;

        return Action.Posible(target);
    }
}