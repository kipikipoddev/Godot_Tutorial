using Godot;
using Hex_Space_Rpg.Events;

namespace Hex_Space_Rpg.Views;

public partial class Base_View<T> : Node2D, IListener<Update_Event>
{
    private T model { get; set; }
    public T Model
    {
        get => model;
        set
        {
            model = value;
            On_Model_Changed();

            if (model != null)
                Mediator.Add_Listener(this);
            else
                Mediator.Remove_Listener(this);
        }
    }

    public void Handle(Update_Event message)
    {
        if (model != null)
            Update();
    }

    protected virtual void On_Model_Changed() { }
    protected virtual void Update() { }

    protected void Remove_Children(Node node)
    {
        foreach (var child in node.GetChildren())
            node.RemoveChild(child);
    }

    protected Label Get_Label(object model, Color? color)
    {
        var label = new Label
        {
            Text = model.ToString(),
            HorizontalAlignment = HorizontalAlignment.Center,
            Theme = new Theme()
        };
        var styleBox = new StyleBoxFlat { BgColor = Colors.White };
        label.AddThemeStyleboxOverride("normal", styleBox);
        label.Theme.SetColor("font_color", "Label", color ?? Colors.Black);
        return label;
    }
}
