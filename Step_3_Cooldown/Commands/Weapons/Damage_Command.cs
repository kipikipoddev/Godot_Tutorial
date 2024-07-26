namespace Hex_Space_Rpg.Commands;

public record Damage_Command : Command, IAmount_Model
{
    private readonly int original_value;

    public int Amount { get; set; }

    public Damage_Command(IEntity_Model entity, int amount)
        : base(entity)
    {
        original_value = amount;
        Amount = amount;
    }

    public override void Send()
    {
        Amount = original_value;
        base.Send();
    }
}