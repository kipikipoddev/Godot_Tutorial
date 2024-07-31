using Godot;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Views;

public partial class Tile_Map : Node2D, IListener<Highlight_Event>
{
    private const string Can_Select = "can_select";
    private TileMap tile_map;
    private IGrid_Model grid;
    private ITargeting_Model targeting;

    private Vector2I selected_cord = new Vector2I(2, 0);
    private Vector2I? origin;
    private Vector2I highlighted;
    private Vector2I[] highlighted_positions;


    public override void _Ready()
    {
        tile_map = GetNode<TileMap>("Tile_Map");
        Mediator.Add_Listener(this);

        targeting = Instances.Get<ITargeting_Model>();
        grid = Instances.Get<IGrid_Model>();
        grid.Converter = p => tile_map.MapToLocal(p.Value);
        Instances.Get<IDistance_Model>().Init(
                tile_map.GetUsedRect().Size,
                p => tile_map.GetCellTileData(0, p) != null);
    }

    public override void _Input(InputEvent e)
    {
        if (!(e is InputEventMouseMotion | e is InputEventMouseButton))
            return;
        var pos = Get_tile_position();
        if (!Can_select(pos))
            return;

        if (highlighted != pos)
            Set_highlighted(pos);
        if (e.IsActionPressed("click") & !grid.Weapon_Entered)
        {
            if (targeting.Is_Weapon_selected)
                Select_Target(pos);
            else
            {
                grid.Clear_Hover();
                origin = pos;
            }
        }
        else if (origin != null && e.IsActionReleased("click"))
            Clicked(pos);
    }

    public void Handle(Highlight_Event evnt)
    {
        Clear_selected(highlighted);
        highlighted_positions = evnt.Positions;
        foreach (var position in highlighted_positions)
            tile_map.SetCell(1, position, 0, selected_cord);
    }

    private void Select_Target(Vector2I pos)
    {
        targeting.Select_Target(pos);
        foreach (var position in highlighted_positions)
            tile_map.EraseCell(1, position);
        highlighted_positions = null;
    }

    private void Clicked(Vector2I pos)
    {
        grid.Select(origin.Value, pos);
        Clear_selected(origin.Value);
        origin = null;
    }

    private void Set_highlighted(Vector2I pos)
    {
        if (highlighted != origin)
            Clear_selected(highlighted);
        if (origin == null)
            grid.Hover(pos);
        highlighted = pos;
        tile_map.SetCell(1, pos, 0, selected_cord);
    }

    private void Clear_selected(Vector2I? pos)
    {
        if (pos.HasValue && (!highlighted_positions?.Contains(pos.Value) ?? true))
            tile_map.EraseCell(1, pos.Value);
    }

    private Vector2I Get_tile_position()
    {
        return tile_map.LocalToMap(GetGlobalMousePosition());
    }

    private bool Can_select(Vector2I pos)
    {
        var tile_data = tile_map.GetCellTileData(0, pos);
        return tile_data != null && tile_data.GetCustomData(Can_Select).AsBool();
    }
}