namespace Hex_Space_Rpg.Models;

public abstract class Action_Model : IAction_Model
{
    protected readonly IWeapon_Model Owner;

    public bool On_Friendly { get; protected set; }

    public Action_Model(IWeapon_Model owner)
    {
        Owner = owner;
        On_Friendly = true;
    }

    public virtual void Perform(IEntity_Model target)
    {
    }

    public bool Posible(IEntity_Model target)
    {
        var is_same_team = target.Team.Equals(Owner.Owner.Team);
        return !(On_Friendly ^ is_same_team);
    }
}