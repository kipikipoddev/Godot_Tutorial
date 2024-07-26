using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Model :
    IWeapon_Model,
    IHandler<Weapon_Fired_Command>,
    IListener<Update_Event>
{
    private readonly ISpaceship_Model owner;

    public string Name { get; }
    public ITimer_Model Cooldown { get; }
    public IAction_Model[] Actions { get; }

    public Weapon_Model(Weapon_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        this.owner = owner;
        Actions = data.Actions.Select(a => a.Map(owner)).ToArray();
        Cooldown = new Timer_Model(data.Cooldown_Time);

        Mediator.Add_Handler(this);
        Mediator.Add_Listener(this);
    }

    public bool Is_Available()
    {
        if (!owner.Is_Alive | !Cooldown.Done)
            return false;
        return !owner.Effects.OfType<Stun_Model>().Any();
    }

    public bool Posible(IEntity_Model target)
    {
        if (target == null || !Is_Available())
            return false;

        if (owner.Effects.OfType<Stun_Model>().Any())
            return false;

        if (target.Effects.OfType<Stasis_Model>().Any())
            return false;

        return Actions.First().Posible(target);
    }

    public void Handle(Weapon_Fired_Command cmd)
    {
        if (!Posible(cmd.Target))
            return;
        foreach (var action in Actions)
            action.Perform(cmd.Target);
        new Timer_Command(Cooldown).Send();
    }

    public void Handle(Update_Event evnt)
    {
        if (!owner.Is_Alive & Cooldown.Running)
            new Timer_Command(Cooldown, Timer_Action.Stop).Send();
    }
}