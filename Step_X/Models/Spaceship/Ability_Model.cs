using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Ability_Model : IAbility_Model, IHandler<Fire_Ability_Command>
{
    public string Name { get; }
    public int Range { get; }
    public ISpaceship_Model Owner { get; }
    public ITimer_Model Cooldown { get; }
    public IType_Model Type { get; }
    public IAction_Model Action { get; }
    public IAction_Model Self_Action { get; }

    public Ability_Model(Ability_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        Owner = owner;
        Action = data.Action.Map(this);
        Self_Action = data.Self_Action?.Map(this);
        Cooldown = new Timer_Model(data.Cooldown_Time);
        Type = new Type_Model(data.Type);
        Range = data.Range;
        new Ability_Fire_Model(this);

        Mediator.Add_Handler(this);
    }

    public bool Posible(IEntity_Model target)
    {
        if (!Owner.Is_Alive | Cooldown.Running | Owner.Is_Stun())
            return false;

        if (target == null || !target.Is_Alive)
            return false;

        if (target.Is_Stasis() | target.Position.Is_Moving)
            return false;

        if (!Action.Posible(target))
            return false;

        if (Owner.Position.Is_Moving)
            return false;

        var distance = Owner.Position.Get_Distance(target.Position.Value);
        return distance <= Range;
    }

    public void Handle(Fire_Ability_Command cmd)
    {
        if (!Posible(cmd.Target))
            return;

        Action.Perform(cmd.Target);
        Self_Action?.Perform(Owner);
        var interval = Owner.Get_Buffed(Buff_Type.Cooldown, Cooldown.Interval);
        new Timer_Command(Cooldown, Timer_Action.Start, interval).Send();
    }
}