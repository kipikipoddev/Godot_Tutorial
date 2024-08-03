namespace Hex_Space_Rpg.Interfaces;

public enum State
{
    Not_Started,
    In_Progress,
    Pause,
    Done
}

public interface ITimer_Model
{
    double Current { get; }
    int Interval { get; }
    State State { get; }

    bool Running => State == State.In_Progress | State == State.Pause;
    bool Done => State == State.Done;
}