using Godot;

namespace Hex_Space_Rpg.Views;

public partial class Entity_View : Base_View<IEntity_Model>
{
    private Bar hp_Bar;
    private Bar shield_bar;
    private Label name_label;
    private Label movment_label;
    private VBoxContainer effects_vbox;

    public override void _Ready()
    {
        hp_Bar = GetNode<Bar>("Hp_Bar");
        shield_bar = GetNode<Bar>("Shield_Bar");
        name_label = GetNode<Label>("Name_Label");
        movment_label = GetNode<Label>("Movment_Label");
        effects_vbox = GetNode<VBoxContainer>("Effects");
    }

    protected override void On_Model_Changed()
    {
        hp_Bar.Model = Model.Hp;
        shield_bar.Model = Model.Shield;
        name_label.Text = Model.Name;
        name_label.Modulate = Model.Team.Color;
        hp_Bar.Show_Details = () => Model.Is_Hovering;
        shield_bar.Show_Details = () => Model.Is_Hovering;
    }

    public override void _Process(double delta)
    {
        movment_label.Text = Model.Position.Movment_Cooldown.ToString();
    }

    protected override void Update()
    {
        Update_Visible();
        Update_Effects();
    }

    private void Update_Visible()
    {
        var is_hovering = Model.Is_Hovering;
        name_label.Visible = is_hovering;
        hp_Bar.Visible = Model.Is_Alive;
        shield_bar.Visible = Model.Is_Alive & Model.Shield.Not_Min;
        movment_label.Visible = Model.Position.Movment_Cooldown.Running;
    }

    private void Update_Effects()
    {
        effects_vbox.Visible = Model.Is_Alive & Model.Is_Hovering;
        if (effects_vbox.Visible)
            Add_Effects();
    }

    private void Add_Effects()
    {
        Remove_Children(effects_vbox);
        foreach (var effect in Model.Effects)
            effects_vbox.AddChild(Get_Label(effect.Name, null));
    }
}
