using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Core;

public record Command : Base
{
    public object Model_Object { get; }

    public Command(object model_object, bool is_send = true)
    {
        Model_Object = model_object;
        if (is_send)
            Send();
    }

    public virtual void Send()
    {
        Start();
        Mediator.Invoke(this);
        if (Indentation == 1)
            new Update_Event();
        End();
    }
}
