using FigmaJsonRenderer.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FigmaJsonRenderer;

public sealed class IconComponentRenderer : ComponentRenderer
{
    public IconComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        string color = Resolve(component, "color", component.Color ?? "#000000");

        Path path = new()
        {
            Data = CreateIconGeometry(component.IconName ?? component.Name),
            Fill = FigmaRenderHelpers.BrushFrom(color),
            Stretch = Stretch.Uniform
        };

        return ApplyFrame(
            new Viewbox
            {
                Stretch = Stretch.Uniform,
                Child = path
            },
            component);
    }

    private static Geometry CreateIconGeometry(string? iconName)
    {
        string data = iconName?.ToLowerInvariant() switch
        {
            "arrow_left" => "M 74 12 L 26 60 L 74 108 L 85 97 L 48 60 L 85 23 Z",
            "arrow_right" => "M 46 12 L 94 60 L 46 108 L 35 97 L 72 60 L 35 23 Z",
            _ => "M 10 10 H 110 V 110 H 10 Z"
        };

        return Geometry.Parse(data);
    }
}
