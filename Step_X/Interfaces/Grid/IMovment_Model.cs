namespace Hex_Space_Rpg.Interfaces;

public enum Moving_States
{
    Cant_Move, Can_Move, Moving_From, Moving_To
}

public interface IMovment_Model : IState_Machine<Moving_States>
{
    bool Is_Moving => State == Moving_States.Moving_From | State == Moving_States.Moving_To;
    bool Can_Move { get; }
    IRange_Model Movment_Charges { get; }
}