using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Shield_Model : Range_Model, IHandler<Damage_Command>
{
    private readonly IEntity_Model owner;

    public Shield_Model(IEntity_Model owner, int Shield)
        : base(Shield)
    {
        this.owner = owner;
        Mediator.Add_Handler<Damage_Command>(this, owner);
    }

    public void Handle(Damage_Command cmd)
    {
        if (Amount == 0)
            return;
        var new_value = Amount - cmd.Amount;
        if (new_value < 0)
        {
            cmd.Amount = -new_value;
            new Damage_Event(owner, Amount, true);
            Amount = 0;
        }
        else
        {
            Amount = new_value;
            new Damage_Event(owner, cmd.Amount, true);
            cmd.Amount = 0;
        }
    }
}