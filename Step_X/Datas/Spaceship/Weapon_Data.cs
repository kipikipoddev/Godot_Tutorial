using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Datas;

public class Ability_Data : Named_Data
{
    public int Cooldown_Time;
    public int Active_Time;
    public int Range;
    public Type_Data Type;
    public Action_Data Action;
    public Action_Data Self_Action;

    public IAbility_Model Map(ISpaceship_Model owner)
    {
        return new Ability_Model(this, owner);
    }
}
