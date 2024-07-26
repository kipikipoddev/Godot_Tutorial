using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Models;

public class Type_Model : IType_Model
{
    private readonly Type_Data uppper;

    public string Name { get; }
    public Color Color { get; }

    public Type_Model(Type_Data data)
    {
        Name = data.Name;
        Color = data.Color;
        uppper = data.Upper;
    }

    public bool Is_Under(IType_Model type)
    {
        return type.Name == Name || Is_Under(type.Name, uppper);
    }

    private static bool Is_Under(string name, Type_Data data)
    {
        return data != null && (data.Name == name || Is_Under(name, data.Upper));
    }
}