using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Hp_Model : Range_Model, IHandler<Damage_Command>, IHandler<Heal_Command>
{
    public Hp_Model(int hp, IEntity_Model owner)
        : base(hp)
    {
        Mediator.Add_Handler<Heal_Command>(this, owner);
        Mediator.Add_Handler<Damage_Command>(this, owner);
    }

    public void Handle(Damage_Command cmd)
    {
        Amount -= cmd.Amount;
    }

    public void Handle(Heal_Command cmd)
    {
        Amount += cmd.Amount;
    }

    public override string ToString()
    {
        return Amount == 0 ? "Dead" : base.ToString();
    }
}