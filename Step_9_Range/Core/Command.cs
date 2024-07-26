using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Core;

public record Command : Base
{
    public bool Is_Valid { get; set; }
    public object Model_Object { get; }

    public Command(object model_object)
    {
        Is_Valid = true;
        Model_Object = model_object;
    }

    public virtual bool Send()
    {
        Start();
        Mediator.Invoke(this);
        if (Indentation == 1)
            new Update_Event();
        End();
        return Is_Valid;
    }
}
