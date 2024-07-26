using Godot;
using Hex_Space_Rpg.Definitions;

namespace Hex_Space_Rpg.Views;

public partial class Spaceship_View : Base_View<ISpaceship_Model>
{
    [Export]
    public Spaceship_Resource Spaceship;

    [Export]
    public Team_Resource Team;

    [Export]
    public Vector2I Start_Position;

    private Hp_Bar hp_Bar;
    private Label name_label;
    private Label shield_label;
    private Label armor_label;
    private VBoxContainer effects_vbox;
    private Weapons_View weapons_view;
    private ITargeting_Model targeting;
    private IGrid_Model grid;
    
    private bool is_hovering => grid.Hovering == Model.Position.Value;

    public Spaceship_View()
    {
        targeting = Instances.Get<ITargeting_Model>();
        grid = Instances.Get<IGrid_Model>();
    }

    public override void _Ready()
    {
        hp_Bar = GetNode<Hp_Bar>("Hp_Bar");
        name_label = GetNode<Label>("Name_Label");
        shield_label = GetNode<Label>("Shield_Label");
        armor_label = GetNode<Label>("Armor_Label");
        effects_vbox = GetNode<VBoxContainer>("Effects");
        weapons_view = GetNode<Weapons_View>("Weapons_View");

        Model = Spaceship.Map().Map(Team.Map(), Start_Position);
        weapons_view.Model = Model.Weapons;
        hp_Bar.Model = Model.Hp;
        name_label.Text = Model.Name;
        armor_label.Text = Model.Armor.ToString();
    }

    protected override void Update()
    {
        Position = grid.Converter(Model.Position);
        shield_label.Text = Model.Shield.ToString();

        Update_Visible();
        Update_Effects();
        Update_Team();
    }

    private void Update_Visible()
    {
        weapons_view.Visible = is_hovering & !targeting.Is_Weapon_selected & Model.Is_Alive;
        name_label.Visible = is_hovering;
        hp_Bar.Show_Details = is_hovering;
        armor_label.Visible = Model.Armor.Amount > 0 & is_hovering;
        shield_label.Visible = Model.Shield.Not_Min;
    }

    private void Update_Effects()
    {
        effects_vbox.Visible = Model.Is_Alive;
        if (effects_vbox.Visible)
            Add_Effects();
    }

    private void Add_Effects()
    {
        Remove_Children(effects_vbox);
        foreach (var effect in Model.Effects)
            effects_vbox.AddChild(Get_Label(effect.Name, null));
    }

    private void Update_Team()
    {
        var color = Model.Team.Color;
        name_label.Modulate = color;
        shield_label.Modulate = color;
    }
}
