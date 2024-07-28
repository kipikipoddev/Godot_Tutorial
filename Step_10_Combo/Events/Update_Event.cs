namespace Hex_Space_Rpg.Events;

public record Update_Event : Event
{
    protected override void End()
    {
        if (Indentation == 1)
            new UI_Update_Event();
        base.End();
    }
}