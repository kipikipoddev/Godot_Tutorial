using Hex_Space_Rpg.Datas;
using Godot;

namespace Hex_Space_Rpg.Models;

public class Team_Model : ITeam_Model
{
    public string Name { get; private set; }
    public Color Color { get; private set; }

    public Team_Model(Team_Data data)
    {
        Name = data.Name;
        Color = data.Color;
    }

    public override bool Equals(object obj)
    {
        return Name.Equals((obj as ITeam_Model)?.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}