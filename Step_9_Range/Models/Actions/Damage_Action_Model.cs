using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;

namespace Hex_Space_Rpg.Models;

public class Damage_Action_Model : Action_Model, ITyped
{
    private readonly int amount;

    public IType_Model Type { get; }

    public Damage_Action_Model(Damage_Data data, ISpaceship_Model owner)
        : base(owner)
    {
        On_Friendly = false;
        Type = new Type_Model(data.Type);
        amount = data.Amount;
    }

    public override void Perform(IEntity_Model target)
    {
        new Damage_Command(target, amount, Type).Send();
    }
}