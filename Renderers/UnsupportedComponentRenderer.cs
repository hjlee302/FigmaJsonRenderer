using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigmaJsonRenderer;

public sealed class UnsupportedComponentRenderer : ComponentRenderer
{
    public UnsupportedComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        return ApplyFrame(
            new Border
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(2),
                Child = new TextBlock
                {
                    Text = component.Type,
                    Foreground = Brushes.Red,
                    FontSize = 24,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            },
            component);
    }
}
