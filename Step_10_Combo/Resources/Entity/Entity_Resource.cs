using Godot;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Definitions;

[GlobalClass]
public partial class Entity_Resource : Named_Resource
{
    [Export(PropertyHint.Range, "1,10")]
    public int Hp;

    [Export]
    public int Max_Shield;

    [Export]
    public int Movment_Cooldown;

    public void Add(Entity_Data data)
    {
        data.Name = Name;
        data.Hp = Hp;
        data.Max_Shield = Max_Shield;
        data.Movment_Cooldown = Movment_Cooldown;
    }
}
