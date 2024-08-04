using Godot;
using Hex_Space_Rpg.Definitions;
using Hex_Space_Rpg.Views;

public partial class Bar_View : Base_View<IRange_Model>
{
    [Export]
    public Ratio_Resource[] Ratios;

    public Func<bool> Show_Details;

    private ColorRect border;
    private ColorRect bar;
    private Label values_label;
    private float width;

    public override void _Ready()
    {
        border = GetNode<ColorRect>("Border");
        bar = GetNode<ColorRect>("Bar");
        values_label = GetNode<Label>("Values_Label");
        width = bar.Size.X;
    }

    protected override void Update()
    {
        var show_details = Show_Details();
        border.Visible = show_details;
        values_label.Visible = show_details;
        values_label.Text = $"{Model.Amount} / {Model.Max}";

        var ratio = Model.Ratio;
        bar.Size = new Vector2(width * ratio, bar.Size.Y);
        foreach (var ratio_resource in Ratios)
            if (ratio_resource.Ratio >= ratio)
                bar.Color = ratio_resource.Color;
    }
}
