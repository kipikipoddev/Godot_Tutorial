namespace Hex_Space_Rpg.Commands;

public enum Timer_Action
{
    Start,
    Pause,
    Stop,
    Resume
}

public record Timer_Command(ITimer_Model Timer, Timer_Action Action = Timer_Action.Start, int Reduction = 0) : Command(Timer)
{
}