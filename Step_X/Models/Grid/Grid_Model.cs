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
    }

    public void Hover(Vector2I pos)
    {
        if (hovering != null)
            new Set_Hover_Command(hovering, false);
        hovering = Get_Model(pos);
        if (hovering != null && !hovering.Movment.Is_Moving)
            new Set_Hover_Command(hovering, true);
    }

    public Tile_Type Get_Type(Vector2I pos)
    {
        var entity = Get_Model(pos);
        if (entity == null)
            return Tile_Type.Empty;
        else
            return entity.Movment.Can_Move ? Tile_Type.Entity : Tile_Type.Immobilized_Entity;
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
