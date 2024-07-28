using Godot;
using Hex_Space_Rpg.Events;

public partial class Main_View : Node2D
{
    public override void _Ready()
    {
        new UI_Update_Event();
    }

    public override void _Process(double delta)
    {
        new Time_Event(delta);
    }
}
