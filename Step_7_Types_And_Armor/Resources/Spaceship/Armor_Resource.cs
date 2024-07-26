using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Armor_Resource : Resource
{
    [Export(PropertyHint.Range, "1,10")]
    public int Amount;

    [Export]
    public Type_Resource Type;

    public Armor_Data Map()
    {
        return new Armor_Data(Amount, Type.Map());
    }
}
