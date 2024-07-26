using Godot;

namespace Hex_Space_Rpg.Events;

public record Highlight_Event(Vector2I[] Positions) : Event
{
}