using Godot;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Views;

public partial class Weapon_View : Base_View<IWeapon_Model>
{
    private const string Disable_Path = "res://Assets/Button_Disable.png";

    private Label label;
    private Button button;
    private Texture2D enable_icon;
    private Texture2D disable_icon;
    private ITargeting_Model targeting;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        button = GetNode<Button>("Button");
        enable_icon = button.Icon;
        disable_icon = ResourceLoader.Load<Texture2D>(Disable_Path);
        targeting = Instances.Get<ITargeting_Model>();
    }

    public override void _Process(double delta)
    {
        label.Text = Model.Cooldown.Running ?
            Model.Cooldown.ToString() :
            Model.Name.Replace(' ', '\n');
    }

    public void On_Mouse_Entered()
    {
        Instances.Get<IGrid_Model>().Entered = true;
    }
    public void On_Mouse_Exited()
    {
        Instances.Get<IGrid_Model>().Entered = false;
    }

    protected override void Update()
    {
        button.Icon = Model.Is_Available() ? enable_icon : disable_icon;
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
