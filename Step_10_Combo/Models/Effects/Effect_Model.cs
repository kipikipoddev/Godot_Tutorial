using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public abstract class Effect_Model : IEffect_Model
{
    protected readonly IEntity_Model Target;
    protected readonly Timer_Model Timer;

    public string Name { get; protected set; }

    public Type_Model Type { get; protected set; }


    public Effect_Model(string name, int time, IEntity_Model target)
    {
        Name = name;
        Timer = new Timer_Model(time, Done);
        Target = target;
        Add_To_Effects();
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
            (existing as Effect_Model).Remove();
        Target.Effects.Add(this);
    }
}