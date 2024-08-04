using Hex_Space_Rpg.Commands;

namespace Hex_Space_Rpg.Models;

public class Range_Model : IRange_Model, IHandler<Add_Amount_Command>
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
        Mediator.Add_Handler(this);
    }

    public void Handle(Add_Amount_Command cmd)
    {
        Amount += cmd.Amount;
    }
}