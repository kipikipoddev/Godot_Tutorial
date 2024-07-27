using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Targeting_Model : ITargeting_Model
{
    public IWeapon_Model Weapon_selected { get; private set; }

    public void Select_Target(Vector2I target)
    {
        var spaceship = Get_Model(target);
        var weapon = Weapon_selected;
        Weapon_selected = null;
        new Fire_Weapon_Command(weapon, spaceship).Send();
    }

    public void Select_Weapon(IWeapon_Model weapon)
    {
        Weapon_selected = weapon;
        new Highlight_Event(Get_Positions());
        new Update_Event();
    }

    public Vector2I[] Get_Positions()
    {
        return Instances.Get_All<ISpaceship_Model>()
            .Where(e => Weapon_selected.Posible(e)).Select(e => e.Position.Value).ToArray();
    }

    private static ISpaceship_Model Get_Model(Vector2I position)
    {
        return Instances.Get_All<ISpaceship_Model>()
            .FirstOrDefault(e => e.Position.Value == position);
    }
}