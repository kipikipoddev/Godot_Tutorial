using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Weapon_Model : IWeapon_Model, IHandler<Fire_Weapon_Command>
{
    public string Name { get; }
    public int Range { get; }

    public ISpaceship_Model Owner { get; }
    public ITimer_Model Cooldown { get; }
    public IType_Model Type { get; }
    public IAction_Model Action { get; }

    public Weapon_Model(Weapon_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        Owner = owner;
        Action = data.Action.Map(this);
        Cooldown = new Timer_Model(data.Cooldown_Time);
        Type = new Type_Model(data.Type);
        Range = data.Range;
        new Weapon_Auto_Fire_Model(this);

        Mediator.Add_Handler(this);
    }

    public bool Posible()
    {
        if (!Owner.Is_Alive | !Cooldown.Done)
            return false;
        return !Owner.Effects.OfType<Stun_Model>().Any();
    }

    public bool Posible(IEntity_Model target)
    {
        if (!Posible())
            return false;

        if (target == null || !target.Is_Alive)
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

    public void Handle(Fire_Weapon_Command cmd)
    {
        if (Posible(cmd.Target))
        {
            Action.Perform(cmd.Target);
            (Cooldown as Timer_Model).Start();
        }
    }
}