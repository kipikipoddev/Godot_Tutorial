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

    private Label dead_label;
    private HBoxContainer armor_hb;
    private Weapons_View weapons_view;
    private ITargeting_Model targeting;
    private IGrid_Model grid;

    public Spaceship_View()
    {
        targeting = Instances.Get<ITargeting_Model>();
        grid = Instances.Get<IGrid_Model>();
    }

    public override void _Ready()
    {
        armor_hb = GetNode<HBoxContainer>("Armor");
        weapons_view = GetNode<Weapons_View>("Weapons_View");
        dead_label = GetNode<Label>("Dead_Label");
        var entity_view = GetNode<Entity_View>("Entity_View");

        Model = Spaceship.Map().Map(Team.Map(), Start_Position);
        entity_view.Model = Model;
        weapons_view.Model = Model.Weapons;
        Set_Armor();
    }

    protected override void Update()
    {
        Position = grid.Converter(Model.Position);

        Update_Visible();
    }

    private void Update_Visible()
    {
        var is_hovering = Model.Is_Hovering;
        weapons_view.Visible = is_hovering & !targeting.Is_Weapon_selected; ;
        armor_hb.Visible = Model.Armor.Length > 0 & is_hovering;
        dead_label.Visible = !Model.Is_Alive;
    }


    private void Set_Armor()
    {
        foreach (var armor_model in Model.Armor)
            armor_hb.AddChild(Get_Label(armor_model.Amount, armor_model.Type.Color));
    }
}
