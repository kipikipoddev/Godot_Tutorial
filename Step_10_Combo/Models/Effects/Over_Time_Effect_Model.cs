using Hex_Space_Rpg.Events;
namespace Hex_Space_Rpg.Models;

public class Over_Time_Effect_Model : Effect_Model
{
    private int left;
    private readonly IAction_Model action;

    public Over_Time_Effect_Model(string name, int times, IAction_Model action, int time, IEntity_Model target)
        : base(name, time, target)
    {
        left = times - 1;
        this.action = action;
        action.Perform(Target);
    }

    protected override void Done()
    {
        action.Perform(Target);

        if (--left == 0)
            Remove();
        else
            Timer.Start();
        new Update_Event();
    }
}