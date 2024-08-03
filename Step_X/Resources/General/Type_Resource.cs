using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Type_Resource : Named_Resource
{
    private static readonly Dictionary<string, Type_Data> name_to_data = new();

    [Export]
    public Type_Resource Parent;

    [Export]
    public Color Color;

    public Type_Data Map()
    {
        if (!name_to_data.ContainsKey(Name))
            name_to_data[Name] = Create();
        return name_to_data[Name];
    }

    private Type_Data Create()
    {
        return new Type_Data()
        {
            Name = Name,
            Color = Color,
            Upper = Parent?.Map()
        };
    }
}
