using Godot;

namespace Hex_Space_Rpg.Interfaces;

public interface IGrid_Model
{
    bool Entered { get; set; }
    Vector2I? Hovering { get; }

    Func<IPosition_Model, Vector2> Converter { get; set; }

    void Clear_Hover();
    void Hover(Vector2I pos);
    void Select(Vector2I origin, Vector2I target);
}