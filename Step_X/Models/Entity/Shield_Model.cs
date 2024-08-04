using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Shield_Model : Range_Model, IHandler<Damage_Command>
{
    public Shield_Model(IEntity_Model owner, int Shield)
        : base(Shield)
    {
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
            Amount = 0;
        }
        else
        {
            Amount = new_value;
            cmd.Amount = 0;
        }
    }
}