namespace Hex_Space_Rpg.Interfaces;

public interface IMovment_Model
{
    bool Is_Moving { get; }
    IRange_Model Movment_Charges { get; }
}