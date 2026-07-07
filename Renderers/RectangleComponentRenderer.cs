using FigmaJsonRenderer.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace FigmaJsonRenderer;

public sealed class RectangleComponentRenderer : ComponentRenderer
{
    public RectangleComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        string background = Resolve(component, "background", component.Background ?? "Transparent");

        return ApplyFrame(
            new Border
            {
                Background = FigmaRenderHelpers.BrushFrom(background),
                BorderBrush = FigmaRenderHelpers.BrushFrom(component.StrokeColor ?? "Transparent"),
                BorderThickness = new Thickness(component.StrokeWidth ?? 0),
                CornerRadius = new CornerRadius(component.BorderRadius ?? 0)
            },
            component);
    }
}
