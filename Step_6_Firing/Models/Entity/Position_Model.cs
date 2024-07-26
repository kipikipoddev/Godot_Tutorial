using Godot;

namespace Hex_Space_Rpg.Models;

public class Position_Model : IPosition_Model
{
    public Vector2I Value { get; private set; }


    public Position_Model(Vector2I position)
    {
        Value = position;
    }
}