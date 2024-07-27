using Godot;

namespace Hex_Space_Rpg.Commands;

public record Move_Command(IPosition_Model Model, Vector2I Position) : Command(Model)
{
}