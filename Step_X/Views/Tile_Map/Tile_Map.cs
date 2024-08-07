using Godot;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Views;

public partial class Tile_Map : Node2D, IListener<UI_Update_Event>
{
    private const string Can_Select = "can_select";
    private Dictionary<Tile_Type, Vector2I> type_to_cord;

    private TileMap tile_map;
    private IGrid_Model grid;
    private Vector2I? origin;
    private Vector2I highlighted;

    public override void _Ready()
    {
        tile_map = GetNode<TileMap>("Tile_Map");
        type_to_cord = new Dictionary<Tile_Type, Vector2I>
        {
            [Tile_Type.Empty] = new Vector2I(4, 0),
            [Tile_Type.Entity] = new Vector2I(3, 0),
            [Tile_Type.Immobilized_Entity] = new Vector2I(2, 0)
        };

        grid = Instances.Get<IGrid_Model>();
        grid.Converter = p => tile_map.MapToLocal(p.Value);
        Instances.Get<IDistance_Model>().Init(tile_map.GetUsedRect().Size, Can_select);
        Mediator.Add_Listener(this);
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
        if (e.IsActionPressed("click"))
        {
            grid.Clear_Hover();
            origin = pos;
        }
        else if (origin != null && e.IsActionReleased("click"))
            Clicked(pos);
    }

    public void Handle(UI_Update_Event evnt)
    {
        Set_Highlighted_Cell();
    }

    private void Set_Highlighted_Cell()
    {
        var type = grid.Get_Type(highlighted);
        if (type != Tile_Type.Empty & origin == null)
            grid.Hover(highlighted);
        else
            grid.Clear_Hover();
        tile_map.SetCell(1, highlighted, 0, type_to_cord[type]);
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
        highlighted = pos;
        Set_Highlighted_Cell();
    }

    private void Clear_selected(Vector2I? pos)
    {
        if (pos.HasValue)
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