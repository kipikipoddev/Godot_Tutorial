namespace Hex_Space_Rpg.Datas;

public abstract record Action_Data
{
    public abstract IAction_Model Map(IWeapon_Model owner);
}
