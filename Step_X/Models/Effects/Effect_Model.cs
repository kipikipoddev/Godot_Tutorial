using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public abstract class Effect_Model : IEffect_Model, IHandler<Remove_Effect_Command>
{
    protected readonly IEntity_Model Target;

    public ITimer_Model Timer { get; private set; }
    public string Name { get; protected set; }
    public Type_Model Type { get; private set; }
    public abstract bool Is_Friendly { get; }

    public Effect_Model(string name, int time, IEntity_Model target)
    {
        Name = name;
        Timer = new Timer_Model(time, Done);
        Target = target;
        Add_To_Effects();
        Mediator.Add_Handler(this);
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
            new Remove_Effect_Command(existing);
        Target.Effects.Add(this);
    }
}