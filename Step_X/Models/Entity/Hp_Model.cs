using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Hp_Model : Range_Model, IHandler<Damage_Command>, IHandler<Repair_Command>
{
    private readonly IEntity_Model owner;


    public Hp_Model(int hp, IEntity_Model owner)
        : base(hp)
    {
        this.owner = owner;
        Mediator.Add_Handler<Repair_Command>(this, owner);
        Mediator.Add_Handler<Damage_Command>(this, owner);
    }

    public void Handle(Damage_Command cmd)
    {
        if (cmd.Amount == 0)
            return;

        Amount -= cmd.Amount;
        new Damage_Event(owner, cmd.Amount, false);
    }

    public void Handle(Repair_Command cmd)
    {
        Amount += cmd.Amount;
    }
}