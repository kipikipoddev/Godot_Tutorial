using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Timer_Model :
    ITimer_Model,
    IListener<Time_Event>,
    IHandler<Timer_Command>
{
    public double Current { get; private set; }
    public double Interval { get; protected set; }
    public State State { get; private set; }

    public Timer_Model(int interval)
    {
        Interval = interval;
        if (Interval > 0)
            Start();
        Mediator.Add_Handler(this);
    }

    public void Handle(Time_Event evt)
    {
        Current -= evt.Delta;
        if (Current <= 0 && State == State.In_Progress)
            Done();
    }
    public override string ToString()
    {
        return Current.ToString("F1");
    }

    public void Handle(Timer_Command cmd)
    {
        switch (cmd.Action)
        {
            case Timer_Action.Start:
                Start();
                break;
            case Timer_Action.Pause:
                Pause();
                break;
            case Timer_Action.Stop:
                Stop();
                break;
            case Timer_Action.Resume:
                Resume();
                break;
        }
    }

    private void Resume()
    {
        if (State == State.Pause)
        {
            State = State.In_Progress;
            Mediator.Add_Listener(this);
        }
    }

    private void Pause()
    {
        if (State == State.In_Progress)
        {
            Mediator.Remove_Listener(this);
            State = State.Pause;
        }
    }

    private void Done()
    {
        State = State.Done;
        Mediator.Remove_Listener(this);
        new Update_Event();
    }

    private void Start()
    {
        Current = Interval;
        State = State.In_Progress;
        Mediator.Add_Listener(this);
    }

    private void Stop()
    {
        State = State.Not_Started;
        Mediator.Remove_Listener(this);
    }
}