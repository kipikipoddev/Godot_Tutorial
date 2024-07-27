using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Shield_Model :
    Range_Model,
    IHandler<Damage_Command>,
    IHandler<Add_Shield_Command>
{
    public Shield_Model(ISpaceship_Model owner, int max_shield)
        : base(max_shield)
    {
        Mediator.Add_Handler<Damage_Command>(this, owner);
        Mediator.Add_Handler<Add_Shield_Command>(this, owner);
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

    public void Handle(Add_Shield_Command cmd)
    {
        Amount += cmd.Amount;
    }

    public override string ToString()
    {
        return $"{Amount:d2} / {Max:d2}";
    }
}