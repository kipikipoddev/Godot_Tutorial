using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Timer_Model : ITimer_Model, IListener<Time_Event>
{
    private readonly Action done_action;

    public double Current { get; private set; }
    public double Interval { get; protected set; }
    public State State { get; private set; }

    public Timer_Model(int interval, Action? done_action = null)
    {
        Interval = interval;
        this.done_action = done_action;
        if (Interval > 0)
            Start();
    }

    public void Handle(Time_Event evt)
    {
        Current -= evt.Delta;
        if (Current <= 0 && State == State.In_Progress)
            Done();
    }

    public void Resume()
    {
        if (State != State.Pause)
            return;
        State = State.In_Progress;
        Mediator.Add_Listener(this);
    }

    public void Pause()
    {
        if (State != State.In_Progress)
            return;
        Mediator.Remove_Listener(this);
        State = State.Pause;
    }

    public void Start()
    {
        if (State == State.In_Progress)
            return;
        Current = Interval;
        State = State.In_Progress;
        Mediator.Add_Listener(this);
    }

    public void Stop()
    {
        State = State.Not_Started;
        Mediator.Remove_Listener(this);
    }

    public void Reset()
    {
        Stop();
        Start();
    }

    private void Done()
    {
        State = State.Done;
        Mediator.Remove_Listener(this);
        done_action?.Invoke();
        new Update_Event();
    }
}