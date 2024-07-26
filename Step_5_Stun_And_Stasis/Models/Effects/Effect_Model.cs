using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public abstract class Effect_Model : IEffect_Model, IListener<Update_Event>, IHandler<Remove_Effect_Command>
{
    protected readonly IEntity_Model Target;
    protected readonly ITimer_Model Timer;

    public string Name { get; protected set; }

    public Effect_Model(string name, int time, IEntity_Model target)
    {
        Name = name;
        Timer = new Timer_Model(time);
        Target = target;
        Mediator.Add_Listener(this, true);
        Mediator.Add_Handler(this);
        Add_To_Effects();
    }

    public virtual void Handle(Update_Event evnt)
    {
        if (Timer.Done)
            Done();
    }

    public void Handle(Remove_Effect_Command cmd)
    {
        Remove();
    }

    public void Remove()
    {
        Mediator.Remove_Listener(this);
        Target.Effects.Remove(this);
    }

    protected virtual void Done()
    {
        Remove();
    }

    private void Add_To_Effects()
    {
        var existing = Target.Effects.FirstOrDefault(e => e.Name == Name);
        if (existing != null)
            new Remove_Effect_Command(existing).Send();
        Target.Effects.Add(this);
    }
}