using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public class Weapon_Data : Named_Data
{
    public int Cooldown_Time;
    public int Fire_Time;
    public int Range;
    public Type_Data Type;
    public Action_Data[] Actions;

    public IWeapon_Model Map(ISpaceship_Model owner)
    {
        return new Weapon_Model(this, owner);
    }
}
