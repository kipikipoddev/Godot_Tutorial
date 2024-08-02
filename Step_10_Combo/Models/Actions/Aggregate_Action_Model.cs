using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Aggregate_Action_Model : Action_Model
{
    public IAction_Model[] Actions { get; }

    public Aggregate_Action_Model(Aggregate_Data data, IWeapon_Model owner)
        : base(owner)
    {
        Actions = data.Actions.Select(a => a.Map(owner)).ToArray();
        On_Friendly = Actions.First().On_Friendly;
    }

    public override void Perform(IEntity_Model target)
    {
        foreach (var action in Actions)
            action.Perform(target);
    }
}