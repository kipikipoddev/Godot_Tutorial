using Godot;

namespace Hex_Space_Rpg.Interfaces;

public interface IPosition_Model
{
    Vector2I Value { get; }

    ITimer_Model Movment_Cooldown { get; }

    int Get_Distance(Vector2I position);
}