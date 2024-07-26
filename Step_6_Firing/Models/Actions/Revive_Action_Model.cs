using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Revive_Action_Model : Action_Model
{
    private readonly int to_hp;

    public Revive_Action_Model(Revive_Data data, ISpaceship_Model owner)
        : base(owner)
    {
        On_Alive = false;
        to_hp = data.To_Hp;
    }

    public override void Perform(ISpaceship_Model target)
    {
        new Heal_Command(target, to_hp).Send();
        foreach (var action in target.Weapons)
            new Timer_Command(action.Cooldown).Send();
    }
}