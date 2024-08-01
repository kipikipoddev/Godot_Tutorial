namespace Hex_Space_Rpg.Models;

public class Root_Model : Effect_Model
{
    public Root_Model(int time, IEntity_Model target)
        : base("Root", time, target)
    {
    }
}

public static class IEntity_Model_Root_Extention
{
    public static bool Is_Root(this IEntity_Model model)
    {
        return model.Effects.OfType<Root_Model>().Any();
    }
}