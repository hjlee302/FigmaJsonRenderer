using FigmaJsonRenderer.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FigmaJsonRenderer;

public sealed class BlinkingTextComponentRenderer : ComponentRenderer
{
    public BlinkingTextComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        TextBlock textBlock = new()
        {
            Text = component.Text ?? "",
            FontSize = component.FontSize ?? 16,
            Foreground = FigmaRenderHelpers.BrushFrom(component.ColorA ?? component.Color ?? "#000000")
        };

        DispatcherTimer timer = new()
        {
            Interval = TimeSpan.FromMilliseconds(component.IntervalMs ?? 500)
        };
        bool toggle = false;

        timer.Tick += (_, _) =>
        {
            toggle = !toggle;
            textBlock.Foreground = FigmaRenderHelpers.BrushFrom(
                toggle
                    ? component.ColorB ?? component.Color ?? "#000000"
                    : component.ColorA ?? component.Color ?? "#000000");
        };
        timer.Start();

        return ApplyFrame(textBlock, component);
    }
}
