﻿using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Weapon_Model : IWeapon_Model, IHandler<Fire_Weapon_Command>
{
    private readonly Weapon_Fire_Model fire_model;

    public string Name { get; }
    public int Range { get; }
    public ISpaceship_Model Owner { get; }
    public ITimer_Model Cooldown { get; }
    public IType_Model Type { get; }
    public IAction_Model Action { get; }
    public bool Is_Auto_Fire => fire_model.Auto_Fire_Mode;

    public Weapon_Model(Weapon_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        Owner = owner;
        Action = data.Action.Map(this);
        Cooldown = new Timer_Model(data.Cooldown_Time);
        Type = new Type_Model(data.Type);
        Range = data.Range;
        fire_model = new Weapon_Fire_Model(this, data.Auto_Fire);

        Mediator.Add_Handler(this);
    }

    public bool Posible(IEntity_Model target)
    {
        if (!Owner.Is_Alive | Cooldown.Running | Owner.Is_Stun())
            return false;

        if (target == null || !target.Is_Alive)
            return false;

        if (target.Is_Stasis())
            return false;

        if (!Action.Posible(target))
            return false;

        var distance = Owner.Position.Get_Distance(target.Position.Value);
        return distance <= Range;
    }

    public void Handle(Fire_Weapon_Command cmd)
    {
        if (!Posible(cmd.Target))
            return;

        Action.Perform(cmd.Target);
        new Timer_Command(Cooldown).Send();
    }
}