using Godot;

namespace Hex_Space_Rpg.Interfaces;

public interface IDistance_Model
{
    void Init(Vector2I size, Func<Vector2I, bool> in_map);

    int Get_Distance(Vector2I from, Vector2I to);
}