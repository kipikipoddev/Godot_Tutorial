using Hex_Space_Rpg.Events;
using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Core;

public class State_Machine<TStats> : IState_Machine<TStats>, IListener<Update_Event>
    where TStats : struct, Enum
{
    private TStats current;
    private readonly List<State_Machine_Data<TStats>> transitions;
    private readonly Dictionary<TStats, List<Action>> on_transition;

    public TStats State
    {
        get => current;
        protected set
        {
            if (value.Equals(current))
                return;
            current = value;
            if (on_transition.ContainsKey(value))
                foreach (var action in on_transition[value])
                    action();
        }
    }

    protected State_Machine(TStats start = default)
    {
        State = start;
        transitions = new();
        on_transition = new();
        Mediator.Add_Listener(this);
    }

    public void Handle(Update_Event evnt)
    {
        foreach (var data in transitions)
            if (data.From.Equals(State) && data.Condition())
                State = data.To;
    }

    public void On_Enter(TStats state, Action action)
    {
        if (!on_transition.ContainsKey(state))
            on_transition[state] = new();
        on_transition[state].Add(action);
    }

    public void On_Enter_Deleyed(TStats state, int delay, Action action)
    {
        On_Enter(state, () => new Timer_Model(delay, action));
    }

    protected void Add_Transition(TStats from, TStats to, Func<bool> condition)
    {
        if (!from.Equals(to))
            transitions.Add(new State_Machine_Data<TStats>(from, to, condition));
    }

    protected void Add_Any_Transition(TStats to, Func<bool> condition)
    {
        foreach (var state in Enum.GetValues<TStats>())
            Add_Transition(state, to, condition);
    }
}
