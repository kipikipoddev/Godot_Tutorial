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
    private Abilities_View weapons_view;
    private IGrid_Model grid;

    public Spaceship_View()
    {
        grid = Instances.Get<IGrid_Model>();
    }

    public override void _Ready()
    {
        armor_hb = GetNode<HBoxContainer>("Armor");
        weapons_view = GetNode<Abilities_View>("Abilities_View");
        dead_label = GetNode<Label>("Dead_Label");
        var entity_view = GetNode<Entity_View>("Entity_View");

        Model = Spaceship.Map().Map(Team.Map(), Start_Position);
        entity_view.Model = Model;
        weapons_view.Model = Model.Abilities;
        Set_Armor();
    }

    protected override void Update()
    {
        Position = grid.Converter(Model.Position);
        ZIndex = Model.Is_Hovering ? 2 : 1;
        Update_Visible();
    }

    private void Update_Visible()
    {
        weapons_view.Visible = Model.Is_Hovering & Model.Is_Alive;
        armor_hb.Visible = Model.Armor.Length > 0 & Model.Is_Hovering & Model.Is_Alive;
        dead_label.Visible = !Model.Is_Alive;
    }

    private void Set_Armor()
    {
        foreach (var armor_model in Model.Armor)
            armor_hb.AddChild(Get_Label(armor_model.Amount, armor_model.Type.Color));
    }
}