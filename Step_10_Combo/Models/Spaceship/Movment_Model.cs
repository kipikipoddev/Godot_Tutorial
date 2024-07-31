using Godot;
using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Movment_Model : IHandler<Move_Command>
{
    private readonly ISpaceship_Model owner;

    public Movment_Model(ISpaceship_Model owner)
    {
        this.owner = owner;
        Mediator.Add_Handler(this, owner.Position);
    }

    public void Handle(Move_Command cmd)
    {
        cmd.Is_Valid = Can_Move(cmd.Position);
    }

    private bool Can_Move(Vector2I position)
    {
        if (!owner.Is_Alive | owner.Position.Movment_Cooldown.Running)
            return false;
        if (owner.Effects.OfType<Stun_Model>().Any())
            return false;
        return owner.Position.Get_Distance(position) == 1;
    }
}