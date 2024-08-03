using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Team_Resource : Named_Resource
{
    [Export]
    public Color Color;

    public Team_Data Map()
    {
        return new Team_Data()
        {
            Name = Name,
            Color = Color
        };
    }
}
