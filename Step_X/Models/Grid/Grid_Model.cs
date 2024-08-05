using Godot;
using Hex_Space_Rpg.Commands;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Models;

public class Grid_Model : IGrid_Model
{
    private IEntity_Model hovering;

    public Func<IPosition_Model, Vector2> Converter { get; set; }
    public Func<Vector2I, bool> Is_Valid { get; set; }

    public void Clear_Hover()
    {
        if (hovering != null)
            new Set_Hover_Command(hovering, false);
        new UI_Update_Event();
    }

    public void Hover(Vector2I pos)
    {
        if (hovering != null)
            new Set_Hover_Command(hovering, false);
        hovering = Get_Model(pos);
        new Set_Hover_Command(hovering, true);
        new UI_Update_Event();
    }

    public void Select(Vector2I origin, Vector2I target)
    {
        var origin_model = Get_Model(origin);
        var target_model = Get_Model(target);

        if (origin_model != null && target_model == null)
        {
            new Move_Command(origin_model, target);
            Hover(target);
        }
        else
            Clear_Hover();
    }

    private static ISpaceship_Model Get_Model(Vector2I position)
    {
        return Instances.Get_All<ISpaceship_Model>()
            .FirstOrDefault(e => e.Position.Value == position);
    }
}
