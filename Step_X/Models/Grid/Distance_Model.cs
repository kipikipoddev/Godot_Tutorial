using Godot;

namespace Hex_Space_Rpg.Models;

public class Distance_Model : IDistance_Model
{
    private readonly Dictionary<Vector2I, Vector2I[]> surrounding;

    public Distance_Model()
    {
        surrounding = new();
    }

    public void Init(Vector2I size, Func<Vector2I, bool> in_map)
    {
        var positions = Get_Positions(size).Where(in_map).ToArray();
        foreach (var position in positions)
            surrounding[position] = Get_Surrounding(position, positions).ToArray();
    }

    public int Get_Distance(Vector2I from, Vector2I to)
    {
        return Get_Distance(to, new HashSet<Vector2I>() { from });
    }

    private static IEnumerable<Vector2I> Get_Positions(Vector2I size)
    {
        for (int x = -size.X; x < size.X; x++)
            for (int y = -size.Y; y < size.Y; y++)
                yield return new Vector2I(x, y); ;
    }

    private static IEnumerable<Vector2I> Get_Surrounding(Vector2I pos, IEnumerable<Vector2I> positions)
    {
        return Get_Surrounding(pos).Where(p => positions.Contains(p));
    }

    private static IEnumerable<Vector2I> Get_Surrounding(Vector2I pos)
    {
        yield return new Vector2I(pos.X - 1, pos.Y);
        yield return new Vector2I(pos.X, pos.Y - 1);
        yield return new Vector2I(pos.X + 1, pos.Y - 1);
        yield return new Vector2I(pos.X + 1, pos.Y);
        yield return new Vector2I(pos.X, pos.Y + 1);
        yield return new Vector2I(pos.X - 1, pos.Y + 1);
    }

    private int Get_Distance(Vector2I target, HashSet<Vector2I> positions)
    {
        if (positions.Contains(target))
            return 0;
        var surrounding_positions = positions
            .SelectMany(p => surrounding[p])
            .Where(p => !positions.Contains(p))
            .ToHashSet();
        return Get_Distance(target, surrounding_positions) + 1;
    }
}