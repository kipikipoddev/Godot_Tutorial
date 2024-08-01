using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Aggregate_Action_Model : Action_Model
{
    private readonly IAction_Model[] actions;

    public Aggregate_Action_Model(Aggregate_Data data, IWeapon_Model owner)
        : base(owner)
    {
        actions = data.Actions.Select(a => a.Map(owner)).ToArray();
        On_Friendly = actions.First().On_Friendly;
    }

    public override void Perform(IEntity_Model target)
    {
        foreach (var action in actions)
            action.Perform(target);
    }
}