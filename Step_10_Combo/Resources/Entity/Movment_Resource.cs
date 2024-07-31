using Godot;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Movment_Resource : Resource
{
    [Export(PropertyHint.Range, "1,10")]
    public int Recharge_Time;

    [Export(PropertyHint.Range, "1,10")]
    public int Max_Tiles;

    public Movment_Data Map()
    {
        return new Movment_Data()
        {
            Recharge_Time = Recharge_Time,
            Max_Tiles = Max_Tiles
        };
    }
}
