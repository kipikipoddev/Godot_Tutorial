using Godot;

namespace Hex_Space_Rpg.Interfaces;

public interface IPosition_Model
{
    Vector2I Value { get; }
    bool Is_Moving { get; }
    IRange_Model Movment_Charges { get; }
    int Get_Distance(Vector2I position);
}