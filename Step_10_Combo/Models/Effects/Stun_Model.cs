namespace Hex_Space_Rpg.Models;

public class Stun_Model : Effect_Model
{
    public Stun_Model(int time, ISpaceship_Model target)
        : base("Stun", time, target)
    {
    }
}