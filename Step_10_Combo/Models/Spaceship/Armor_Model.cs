using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Armor_Model : IArmor_Model, IHandler<Damage_Command>
{
    public IType_Model Type { get; }
    public int Amount { get; }

    public Armor_Model(Armor_Data data, ISpaceship_Model owner)
    {
        Amount = data.Amount;
        Type = new Type_Model(data.Type);
        Mediator.Add_Handler(this, owner);
    }

    public void Handle(Damage_Command cmd)
    {
        if (cmd.Amount == 0)
            return;
            
        if (cmd.Type.Is_Under(Type))
            cmd.Amount -= Amount;

        cmd.Amount = Math.Max(1, cmd.Amount);
    }
}