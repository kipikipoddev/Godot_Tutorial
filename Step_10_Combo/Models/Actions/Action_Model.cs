namespace Hex_Space_Rpg.Models;

public abstract class Action_Model : IAction_Model
{
    protected readonly IWeapon_Model Owner;
    protected bool On_Alive { get; set; }
    protected bool On_Spaceships { get; set; }

    public bool On_Friendly { get; protected set; }

    public Action_Model(IWeapon_Model owner)
    {
        Owner = owner;
        On_Friendly = true;
        On_Alive = true;
        On_Spaceships = true;
    }

    public virtual void Perform(IEntity_Model target)
    {
        if (On_Spaceships && target is ISpaceship_Model Spaceship)
            Perform(Spaceship);
    }

    public virtual void Perform(ISpaceship_Model target)
    {
    }

    public bool Posible(IEntity_Model target)
    {
        var is_same_team = target.Team.Equals(Owner.Owner.Team);
        if (On_Friendly ^ is_same_team)
            return false;

        if (On_Alive ^ target.Is_Alive)
            return false;

        if (On_Spaceships && !(target is ISpaceship_Model))
            return false;

        return true;
    }
}