using Godot;

namespace Hex_Space_Rpg.Views;

public partial class Weapon_View : Base_View<IWeapon_Model>
{
    [Export]
    public Color Cooldown_Color;

    [Export]
    public Color Block_Color;

    [Export]
    public Color Ideal_Color;

    private Label label;
    private Sprite2D border;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        border = GetNode<Sprite2D>("Border");
    }

    public override void _Process(double delta)
    {
        var name = Model.Name;
        if (Model.In_Cooldown)
            name += " " + Model.Cooldown.Current.ToString("F1");
        label.Text = name.Replace(' ', '\n');
    }

    protected override void Update()
    {
        border.Modulate = Model.In_Cooldown ?
            Cooldown_Color :
            Model.Cant_Shoot ? Block_Color : Ideal_Color;
    }

    protected override void On_Model_Changed()
    {
        var default_settings = label.LabelSettings;
        label.LabelSettings = new LabelSettings
        {
            FontSize = default_settings.FontSize,
            OutlineSize = default_settings.OutlineSize,
            OutlineColor = default_settings.OutlineColor,
            FontColor = Model?.Type?.Color ?? Colors.Black
        };
        Update();
    }
}
