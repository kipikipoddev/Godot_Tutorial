namespace Hex_Space_Rpg.Models;

public class Stun_Model : Root_Model
{
    public Stun_Model(int time, ISpaceship_Model target)
        : base(time, target)
    {
        Name = "Stun";
    }
}

public static class IEntity_Model_Stun_Extention
{
    public static bool Is_Stun(this IEntity_Model model)
    {
        return model.Effects.OfType<Stun_Model>().Any();
    }
}