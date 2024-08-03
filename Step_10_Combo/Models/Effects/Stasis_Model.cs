using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Stasis_Model : Stun_Model
{
    public Stasis_Model(int time, IEntity_Model target)
        : base(time, target)
    {
        Name = "Stasis";
    }

    protected override void Done()
    {
        base.Done();
        foreach (var effect in Target.Effects)
            new Timer_Command(effect.Timer, Timer_Action.Resume).Send();
        if (Target is ISpaceship_Model ship)
            foreach (var ability in ship.Abilities)
                new Timer_Command(ability.Cooldown, Timer_Action.Resume).Send();
    }
}

public static class IEntity_Model_Stasis_Extention
{
    public static bool Is_Stasis(this IEntity_Model model)
    {
        return model.Effects.OfType<Stasis_Model>().Any();
    }
}