using Godot;
using Hex_Space_Rpg.Events;

public partial class Damage_View : Node2D
{
    [Export]
    public Color Damage_Color;

    [Export]
    public Color Shield_Color;

    public Damage_Event Event { get; set; }

    private AnimationPlayer animation;


    public override void _Ready()
    {
        animation = GetNode<AnimationPlayer>("Animation");
        var label = GetNode<Label>("Damage_Label");
        label.LabelSettings.FontColor = Event.Is_Shield ? Shield_Color : Damage_Color;
        label.Text = $"-{Event.Amount}";
    }

    public override void _Process(double delta)
    {
        if (!animation.IsPlaying())
            QueueFree();
    }
}
