namespace Hex_Space_Rpg.Datas;

public record Armor_Data(int Amount, Type_Data Type)
{
    public IArmor_Model Map(ISpaceship_Model owner)
    {
        return Instances.Create<IArmor_Model>(this, owner);
    }
}
