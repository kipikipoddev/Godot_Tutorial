
using Godot;

namespace Hex_Space_Rpg.Interfaces;

public interface ITeam_Model : IName_Model
{
    Color Color { get; }
}