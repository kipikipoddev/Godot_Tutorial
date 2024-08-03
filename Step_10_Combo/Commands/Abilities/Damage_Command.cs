namespace Hex_Space_Rpg.Commands;

public record Damage_Command : Command, IAmount_Model
{
    private readonly int original_value;

    public int Amount { get; set; }
    public IType_Model Type { get; }

    public Damage_Command(IEntity_Model entity, int amount, IType_Model type)
        : base(entity)
    {
        original_value = amount;
        Amount = amount;
        Type = type;
    }

    public override void Send()
    {
        Amount = original_value;
        base.Send();
    }
}