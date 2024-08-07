using Godot;

namespace Hex_Space_Rpg.Interfaces;

public enum Tile_Type
{
    Empty,
    Entity,
    Immobilized_Entity
}

public interface IGrid_Model
{
    Func<IPosition_Model, Vector2> Converter { get; set; }

    void Clear_Hover();
    void Hover(Vector2I pos);
    Tile_Type Get_Type(Vector2I pos);
    void Select(Vector2I origin, Vector2I target);
}