
using Godot;

public interface ITargeting_Model
{
    IWeapon_Model Weapon_selected { get; }

    IEntity_Model Get_Target(IWeapon_Model weapon);
    void Select_Weapon(IWeapon_Model weapon);
    void Select_Target(Vector2I target);

    bool Is_Weapon_selected => Weapon_selected != null;
}