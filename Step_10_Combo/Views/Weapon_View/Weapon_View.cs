using Godot;

namespace Hex_Space_Rpg.Views;

public partial class Weapon_View : Base_View<IWeapon_Model>
{
    [Export]
    public Color Cooldown_Color;

    [Export]
    public Color Firing_Color;

    [Export]
    public Color Ideal_Color;

    private Label label;
    private Sprite2D border;

    private ITargeting_Model targeting;
    private IGrid_Model grid;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        border = GetNode<Sprite2D>("Border");
        targeting = Instances.Get<ITargeting_Model>();
        grid = Instances.Get<IGrid_Model>();
    }

    public override void _Process(double delta)
    {
        var name = Model.Name;
        if (Model.Cooldown.Running)
            name += " " + Model.Cooldown.Current.ToString("F1");
        else if (Model.Is_Firing)
            name += " " + Model.Firing.Current.ToString("F1");
        label.Text = name.Replace(' ', '\n');
    }

    public void On_Mouse_Entered()
    {
        grid.Weapon_Entered = true;
    }
    public void On_Mouse_Exited()
    {
        grid.Weapon_Entered = false;
    }

    protected override void Update()
    {
        border.Modulate = Model.Is_Cooldown ? Cooldown_Color : Model.Is_Firing ? Firing_Color : Ideal_Color;
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

    public void On_Button_Pressed()
    {
        targeting.Select_Weapon(Model);
    }
}
