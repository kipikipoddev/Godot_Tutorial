namespace Hex_Space_Rpg.Interfaces;

public interface IEntity_Model : IName_Model
{
    IRange_Model Hp { get; }
    IRange_Model Shield { get; }
    List<IEffect_Model> Effects { get; }
    ITeam_Model Team { get; }
    IPosition_Model Position { get; }

    bool Is_Alive => Hp.Not_Min;
    bool Is_Hovering => Instances.Get<IGrid_Model>().Hovering == Position.Value & Is_Alive;
}