using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Buff_Model : Effect_Model
{
    public int Amount { get; }
    public Buff_Type Buff_Type { get; }
    public override bool Is_Friendly => Amount > 0;

    public Buff_Model(int time, int amount, Buff_Type type, IEntity_Model target)
        : base(type.ToString(), time, target)
    {
        Amount = amount;
        Buff_Type = type;
    }
}

public static class IEntity_Model_Buff_Extention
{
    public static int Get_Buffed(this IEntity_Model model, Buff_Type type, int amount)
    {
        var buff = model.Effects
            .OfType<Buff_Model>()
            .Where(b => b.Buff_Type == type)
            .FirstOrDefault();
        amount += buff?.Amount ?? 0;
        return Math.Max(1, amount);
    }
}