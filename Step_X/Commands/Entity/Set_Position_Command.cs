using Godot;

namespace Hex_Space_Rpg.Commands;

public record Set_Position_Command(IEntity_Model Model, Vector2I Position) : Command(Model)
{
}