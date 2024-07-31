using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Datas;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Weapon_Auto_Fire_Model : IListener<Update_Event>
{
    private readonly IWeapon_Model weapon;

    public Weapon_Auto_Fire_Model(IWeapon_Model weapon)
    {
        this.weapon = weapon;
        Mediator.Add_Listener(this);
    }

    public void Handle(Update_Event evnt)
    {
        if (!weapon.Is_Cooldown & !weapon.Is_Firing)
        {
            var target = Get_Target();
            if (target != null)
                new Fire_Weapon_Command(weapon, target).Send();
        }
    }

    private ISpaceship_Model Get_Target()
    {
        return Instances.Get_All<ISpaceship_Model>()
            .Where(s => weapon.Posible(s))
            .FirstOrDefault();
    }
}