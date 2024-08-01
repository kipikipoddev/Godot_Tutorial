namespace Hex_Space_Rpg.Datas;

public class Weapon_Data : Named_Data
{
    public int Cooldown_Time;
    public int Range;
    public Type_Data Type;
    public Action_Data Action;

    public IWeapon_Model Map(ISpaceship_Model owner)
    {
        return Instances.Create<IWeapon_Model>(this, owner);
    }
}
