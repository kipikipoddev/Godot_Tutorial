using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Model :
    IWeapon_Model,
    IHandler<Weapon_Fired_Command>,
    IHandler<Fire_Weapon_Command>,
    IListener<Update_Event>
{
    private readonly int Fire_Time;
    private readonly ISpaceship_Model owner;

    public string Name { get; }
    public ITimer_Model Cooldown { get; }
    public IAction_Model[] Actions { get; }

    public Weapon_Model(Weapon_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        this.owner = owner;
        Fire_Time = data.Fire_Time;
        Actions = data.Actions.Select(a => a.Map(owner)).ToArray();
        Cooldown = new Timer_Model(data.Cooldown_Time);

        Mediator.Add_Handler<Weapon_Fired_Command>(this);
        Mediator.Add_Handler<Fire_Weapon_Command>(this);
        Mediator.Add_Listener(this);
    }

    public bool Is_Available(bool ignore_firing)
    {
        if (!owner.Is_Alive | !Cooldown.Done)
            return false;
        if (!ignore_firing & owner.Is_Firing)
            return false;
        return !owner.Effects.OfType<Stun_Model>().Any();
    }

    public bool Posible(IEntity_Model target, bool ignore_firing)
    {
        if (target == null || !Is_Available(ignore_firing))
            return false;

        if (owner.Effects.OfType<Stun_Model>().Any())
            return false;

        if (target.Effects.OfType<Stasis_Model>().Any())
            return false;

        return Actions.First().Posible(target);
    }

    public void Handle(Weapon_Fired_Command cmd)
    {
        foreach (var action in Actions)
            action.Perform(cmd.Target);
        new Timer_Command(Cooldown).Send();
    }

    public void Handle(Update_Event evnt)
    {
        if (!owner.Is_Alive & Cooldown.Running)
            new Timer_Command(Cooldown, Timer_Action.Stop).Send();
    }

    public void Handle(Fire_Weapon_Command cmd)
    {
        if (Posible(cmd.Target, false))
            new Start_Weapon_Fire_Command(owner, Fire_Time, this, cmd.Target).Send();
    }
}