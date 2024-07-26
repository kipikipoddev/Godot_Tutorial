
using Godot;

namespace Hex_Space_Rpg.Interfaces;

public interface IType_Model : IName_Model
{
    Color Color { get; }

    bool Is_Under(IType_Model type);
}