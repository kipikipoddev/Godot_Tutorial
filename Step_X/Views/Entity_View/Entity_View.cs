using Godot;

namespace Hex_Space_Rpg.Views;

public partial class Entity_View : Base_View<IEntity_Model>
{
    private Bar hp_Bar;
    private Bar shield_bar;
    private Label name_label;
    private Label movment_label;
    private VBoxContainer pos_effects_vbox;
    private VBoxContainer neg_effects_vbox;
    private Label[] pos_effects_labels;
    private Label[] neg_effects_labels;

    public override void _Ready()
    {
        hp_Bar = GetNode<Bar>("Hp_Bar");
        shield_bar = GetNode<Bar>("Shield_Bar");
        name_label = GetNode<Label>("Name_Label");
        movment_label = GetNode<Label>("Movment_Label");
        pos_effects_vbox = GetNode<VBoxContainer>("Pos_Effects");
        neg_effects_vbox = GetNode<VBoxContainer>("Neg_Effects");
        pos_effects_labels = new Label[4];
        neg_effects_labels = new Label[4];
        for (int i = 0; i < 4; i++)
        {
            pos_effects_labels[i] = GetNode<Label>("%Pos_Label_" + (i + 1));
            neg_effects_labels[i] = GetNode<Label>("%Neg_Label_" + (i + 1));
        }
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

    protected override void Update()
    {
        Update_Visible();
        Update_Effects();
        Update_Movment();
    }

    private void Update_Movment()
    {
        var movment_Charges = Model.Position.Movment_Charges;
        movment_label.Text = $"{movment_Charges.Amount}/{movment_Charges.Max}";
        movment_label.Visible = Model.Is_Alive;
    }

    private void Update_Visible()
    {
        var is_hovering = Model.Is_Hovering;
        name_label.Visible = is_hovering;
        hp_Bar.Visible = Model.Is_Alive;
        shield_bar.Visible = Model.Is_Alive & Model.Shield.Not_Min;
    }

    private void Update_Effects()
    {
        pos_effects_vbox.Visible = Model.Is_Alive & Model.Is_Hovering;
        neg_effects_vbox.Visible = Model.Is_Alive & Model.Is_Hovering;

        var pos = 3;
        var neg = 3;
        foreach (var effect in Model.Effects)
        {
            if (effect.Is_Friendly)
                pos_effects_labels[pos--].Text = effect.Name;
            else
                neg_effects_labels[neg--].Text = effect.Name;
        }
        for (int i = 0; i < 4; i++)
        {
            if (i <= pos)
                pos_effects_labels[i].Text = string.Empty;
            if (i <= neg)
                neg_effects_labels[i].Text = string.Empty;
        }
    }
}
