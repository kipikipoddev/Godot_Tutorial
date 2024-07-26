using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Stun_Model : Effect_Model
{
    public Stun_Model(int time, ISpaceship_Model target)
        : base("Stun", time, target)
    {
    }

    protected override void Done()
    {
        Remove();
        new Timer_Command((Target as ISpaceship_Model).Firing, Timer_Action.Stop);
    }
}