namespace Hex_Space_Rpg.Models;

public class Range_Model : IRange_Model
{
    private int value;

    public int Min { get; protected set; }
    public int Max { get; protected set; }
    public int Amount
    {
        get => value;
        set
        {
            this.@value = Math.Min(Max, Math.Max(Min, value));
        }
    }

    public Range_Model(int max, int? value = null, int min = 0)
    {
        Max = max;
        Min = min;
        Amount = value ?? max;
    }
}