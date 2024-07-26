using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Armor_Model : IArmor_Model, IHandler<Damage_Command>
{
    public int Amount { get; }

    public Armor_Model(int amount, ISpaceship_Model owner)
    {
        Amount = amount;
        Mediator.Add_Handler(this, owner);
    }

    public void Handle(Damage_Command cmd)
    {
        if (cmd.Amount == 0)
            return;
        cmd.Amount = Math.Max(1, cmd.Amount - Amount);
    }

    public override string ToString()
    {
        return $"{Amount:d2}";
    }
}