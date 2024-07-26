using Godot;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Grid_Model : IGrid_Model, IListener<Highlight_Event>
{
    public Vector2I? Hovering { get; private set; }
    public Func<IPosition_Model, Vector2> Converter { get; set; }
    public Func<Vector2I, bool> Is_Valid { get; set; }
    public bool Entered { get; set; }

    public Grid_Model()
    {
        Mediator.Add_Listener(this);
    }

    public void Handle(Highlight_Event evnt)
    {
        Hovering = null;
    }

    public void Clear_Hover()
    {
        Hovering = null;
        new Update_Event();
    }

    public void Hover(Vector2I pos)
    {
        if (!Entered)
        {
            Hovering = pos;
            new Update_Event();
        }
    }

    public void Select(Vector2I origin, Vector2I target)
    {
        var origin_model = Get_Model(origin);
        var target_model = Get_Model(target);

        if (origin_model != null && target_model == null)
            Hovering = target;
        else
        {
            Hovering = null;
            new Update_Event();
        }
    }

    private static ISpaceship_Model Get_Model(Vector2I position)
    {
        return Instances.Get_All<ISpaceship_Model>()
            .FirstOrDefault(e => e.Position.Value == position);
    }
}
