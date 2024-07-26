using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Fire_Model : ITimer_Model, IListener<Update_Event>, IHandler<Start_Weapon_Fire_Command>
{
    private ITimer_Model? timer;
    private IWeapon_Model weapon;
    private IEntity_Model target;

    public double Current => timer?.Current ?? 0;
    public double Interval => timer?.Interval ?? 0;
    public State State => timer?.State ?? State.Not_Started;

    public Fire_Model(ISpaceship_Model Spaceship)
    {
        Mediator.Add_Handler(this, Spaceship);
    }

    public void Handle(Start_Weapon_Fire_Command cmd)
    {
        timer = new Timer_Model(cmd.Time);
        weapon = cmd.Weapon;
        target = cmd.Target;
        Mediator.Add_Listener(this, true);
    }

    public void Handle(Update_Event evnt)
    {
        if (!weapon.Posible(target, true))
            Stop();
        else if (timer?.Done ?? false)
        {
            Stop();
            new Weapon_Fired_Command(weapon, target).Send();
            new Update_Event();
        }
    }

    public override string ToString()
    {
        return timer?.ToString();
    }

    private void Stop()
    {
        new Timer_Command(timer, Timer_Action.Stop).Send();
        timer = null;
        Mediator.Remove_Listener(this);
    }
}