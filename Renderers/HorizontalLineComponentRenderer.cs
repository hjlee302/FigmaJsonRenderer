using FigmaJsonRenderer.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace FigmaJsonRenderer;

public sealed class HorizontalLineComponentRenderer : ComponentRenderer
{
    public HorizontalLineComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        double thickness = component.Thickness ?? component.Height;

        Border line = new()
        {
            Width = component.Width,
            Height = thickness,
            Background = FigmaRenderHelpers.BrushFrom(component.Color ?? component.Background ?? "#000000"),
            ToolTip = component.Name
        };

        return line;
    }
}
