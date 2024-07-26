namespace Hex_Space_Rpg.Core;

public record Event : Base
{
    public Event()
    {
        Start();
        Mediator.Send(this);
        End();
    }
}
