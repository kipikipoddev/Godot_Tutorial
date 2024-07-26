using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Model :
    IWeapon_Model,
    IHandler<Weapon_Fired_Command>
{
    private readonly ISpaceship_Model owner;

    public string Name { get; }
    public IAction_Model[] Actions { get; }

    public Weapon_Model(Weapon_Data data, ISpaceship_Model owner)
    {
        Name = data.Name;
        this.owner = owner;
        Actions = data.Actions.Select(a => a.Map(owner)).ToArray();

        Mediator.Add_Handler(this);
    }

    public bool Is_Available()
    {
        return owner.Is_Alive;
    }

    public bool Posible(IEntity_Model target)
    {
        if (target == null || !Is_Available())
            return false;

        return Actions.First().Posible(target);
    }

    public void Handle(Weapon_Fired_Command cmd)
    {
        if (!Posible(cmd.Target))
            return;
        foreach (var action in Actions)
            action.Perform(cmd.Target);
    }
}