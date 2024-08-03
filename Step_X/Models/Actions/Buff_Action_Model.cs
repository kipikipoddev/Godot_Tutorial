using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Buff_Action_Model : Action_Model, IName_Model
{
    private readonly int time;
    private readonly int amount;
    private readonly Buff_Type type;
    public string Name => type.ToString();

    public Buff_Action_Model(Buff_Data data, IAbility_Model owner)
        : base(owner)
    {
        amount = data.Amount;
        On_Friendly = amount > 0;
        time = data.Effect_Time;
        type = data.Type;
    }

    public override void Perform(IEntity_Model target)
    {
        new Buff_Model(time, amount, type, target);
    }
}