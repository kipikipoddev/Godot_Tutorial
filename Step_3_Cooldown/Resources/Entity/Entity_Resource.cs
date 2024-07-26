using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Entity_Resource : Named_Resource
{
    [Export(PropertyHint.Range, "1,10")]
    public int Hp;
}
