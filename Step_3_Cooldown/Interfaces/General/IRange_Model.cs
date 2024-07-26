
namespace Hex_Space_Rpg.Interfaces;

public interface IRange_Model : IAmount_Model
{
    int Min { get; }
    int Max { get; }
    float Ratio => (float)Amount / (Max - Min);

    bool Is_Min => Amount == Min;
    bool Not_Min => Amount > Min;
}