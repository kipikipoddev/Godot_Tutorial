using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public class Weapon_Data : Named_Data
{
    public Action_Data[] Actions;

    public IWeapon_Model Map(ISpaceship_Model owner)
    {
        return new Weapon_Model(this, owner);
    }
}
