using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Combo_Action_Model : Action_Model
{
    private readonly IAction_Model[] actions;
    private int combo_index;

    public Combo_Action_Model(Combo_Data data, IWeapon_Model owner)
        : base(owner)
    {
        On_Friendly = false;
        actions = data.Actions.Select(a => a.Map(owner)).ToArray();
    }

    public override void Perform(IEntity_Model target)
    {
        actions[combo_index].Perform(target);
        combo_index = (combo_index + 1) % actions.Length;
    }
}