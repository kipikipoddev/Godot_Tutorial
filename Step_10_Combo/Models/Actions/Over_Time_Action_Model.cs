using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Over_Time_Action_Model : Action_Model
{
    private readonly int times;
    private readonly IAction_Model action;
    private readonly string name;
    private readonly int time;


    public Over_Time_Action_Model(Over_Time_Data data, IAbility_Model owner)
        : base(owner)
    {
        times = data.Occur_Times;
        action = data.Action.Map(owner);
        name = data.Name;
        time = data.Time_Between_Occurs;
        On_Friendly = action.On_Friendly;
    }

    public override void Perform(IEntity_Model target)
    {
        new Over_Time_Effect_Model(name, times, action, time, target);
    }
}