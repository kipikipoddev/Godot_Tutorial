using Godot;
using Hex_Space_Rpg.Views;

public partial class Effects_View : Base_View<IEntity_Model>
{
    [Export]
    public LabelSettings LabelSettings;

    [Export]
    public bool Is_Friendly;

    private Label[] labels;

    public override void _Ready()
    {
        labels = new Label[4];
        for (int i = 0; i < 4; i++)
        {
            labels[i] = GetNode<Label>("%Label_" + (i + 1));
            labels[i].LabelSettings = LabelSettings;
        }
    }

    protected override void Update()
    {
        Visible = Model.Is_Alive & Model.Is_Hovering;

        var index = 0;

        foreach (var effect in Model.Effects)
            if (!(effect.Is_Friendly ^ Is_Friendly))
                labels[index++].Text = effect.Name;

        for (int i = 0; i < 4; i++)
            if (i >= index)
                labels[i].Text = string.Empty;
    }
}
