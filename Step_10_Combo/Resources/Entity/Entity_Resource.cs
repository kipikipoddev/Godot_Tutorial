using Godot;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Entity_Resource : Named_Resource
{
    [Export(PropertyHint.Range, "1,10")]
    public int Hp;

    [Export(PropertyHint.Range, "1,10")]
    public int Shield;

    [Export]
    public Movment_Resource Movment;

    public void Add(Entity_Data data)
    {
        data.Name = Name;
        data.Hp = Hp;
        data.Shield = Shield;
        data.Movment = Movment.Map();
    }
}
