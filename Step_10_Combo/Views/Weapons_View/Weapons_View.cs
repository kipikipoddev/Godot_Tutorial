namespace Hex_Space_Rpg.Views;

public partial class Weapons_View : Base_View<IWeapon_Model[]>
{
    private Weapon_View[] weapon_views;

    public IEntity_Model Target { get; set; }

    public override void _Ready()
    {
        weapon_views = new Weapon_View[4];
        weapon_views[0] = GetNode<Weapon_View>("Weapon_1");
        weapon_views[1] = GetNode<Weapon_View>("Weapon_2");
        weapon_views[2] = GetNode<Weapon_View>("Weapon_3");
        weapon_views[3] = GetNode<Weapon_View>("Weapon_4");
    }

    protected override void On_Model_Changed()
    {
        for (int i = 0; i < Model.Length; i++)
            weapon_views[i].Model = Model[i];
        for (int i = Model.Length; i < weapon_views.Length; i++)
            weapon_views[i].QueueFree();
    }
}