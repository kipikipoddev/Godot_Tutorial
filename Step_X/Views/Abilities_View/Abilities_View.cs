namespace Hex_Space_Rpg.Views;

public partial class Abilities_View : Base_View<IAbility_Model[]>
{
    private Ability_View[] weapon_views;

    public IEntity_Model Target { get; set; }

    public override void _Ready()
    {
        weapon_views = new Ability_View[4];
        weapon_views[0] = GetNode<Ability_View>("Ability_1");
        weapon_views[1] = GetNode<Ability_View>("Ability_2");
        weapon_views[2] = GetNode<Ability_View>("Ability_3");
        weapon_views[3] = GetNode<Ability_View>("Ability_4");
    }

    protected override void On_Model_Changed()
    {
        for (int i = 0; i < Model.Length; i++)
            weapon_views[i].Model = Model[i];
        for (int i = Model.Length; i < weapon_views.Length; i++)
            weapon_views[i].QueueFree();
    }
}