namespace Hex_Space_Rpg.Events;

public record Update_Event : Event
{
    protected override void End()
    {
        new UI_Update_Event();
        base.End();
    }
}