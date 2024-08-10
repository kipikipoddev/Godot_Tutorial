using Godot;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Views;

public partial class Entity_View : Base_View<IEntity_Model>, IListener<Move_Event>, IListener<Damage_Event>
{
    private PackedScene damage_scene = (PackedScene)ResourceLoader.Load("res://Views/Damage_View/Damage_View.tscn");
    private Bar_View hp_Bar;
    private Bar_View shield_bar;
    private Label name_label;
    private Label movment_label;
    private Effects_View neg_effects;
    private Effects_View pos_effects;
    private Sprite2D entity_modulate_sprite;

    private AnimationPlayer animation;

    public override void _Ready()
    {
        hp_Bar = GetNode<Bar_View>("Hp_Bar");
        shield_bar = GetNode<Bar_View>("Shield_Bar");
        name_label = GetNode<Label>("Name_Label");
        movment_label = GetNode<Label>("Movment_Label");
        neg_effects = GetNode<Effects_View>("Neg_Effects");
        pos_effects = GetNode<Effects_View>("Pos_Effects");
        entity_modulate_sprite = GetNode<Sprite2D>("Entity_Modulate_Sprite");
        animation = GetNode<AnimationPlayer>("Animation");
        Mediator.Add_Listener<Move_Event>(this);
        Mediator.Add_Listener<Damage_Event>(this);
    }

    public void Handle(Move_Event evnt)
    {
        if (evnt.Model == Model)
            animation.Play("Move");
    }

    public void Handle(Damage_Event evnt)
    {
        if (evnt.Model != Model)
            return;
        var damage_View = damage_scene.Instantiate() as Damage_View;
        damage_View.Event = evnt;
        AddChild(damage_View);
    }

    protected override void On_Model_Changed()
    {
        hp_Bar.Model = Model.Hp;
        shield_bar.Model = Model.Shield;
        name_label.Text = Model.Name;
        entity_modulate_sprite.Modulate = Model.Team.Color;
        hp_Bar.Show_Details = () => Model.Is_Hovering;
        shield_bar.Show_Details = () => Model.Is_Hovering;
        neg_effects.Model = Model;
        pos_effects.Model = Model;
    }

    protected override void Update()
    {
        Update_Visible();
        Update_Movment();
    }

    private void Update_Movment()
    {
        var movment_Charges = Model.Movment.Movment_Charges;
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
}
