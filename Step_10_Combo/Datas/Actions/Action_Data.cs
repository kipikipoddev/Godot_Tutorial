namespace Hex_Space_Rpg.Datas;

public abstract record Action_Data
{
    public abstract IAction_Model Map(IAbility_Model owner);
}
