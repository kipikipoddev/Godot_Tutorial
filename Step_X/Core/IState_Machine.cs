namespace Hex_Space_Rpg.Core;

public interface IState_Machine<TState>
    where TState : Enum
{
    TState State { get; }

    void On_Enter(TState state, Action action);

    void On_Enter_Deleyed(TState state, int delay, Action action);
}
