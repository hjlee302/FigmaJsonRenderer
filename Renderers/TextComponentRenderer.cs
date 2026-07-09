using FigmaJsonRenderer.Helpers;
using FigmaJsonRenderer.Renderers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigmaJsonRenderer;

public sealed class TextComponentRenderer : ComponentRenderer
{
    public TextComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        string text = Resolve(component, "text", component.Text ?? "");

        FigmaTextElement textElement = new()
        {
            Text = text,
            FontSize = component.FontSize ?? 16,
            Foreground = FigmaRenderHelpers.BrushFrom(component.Color ?? "#000000"),
            FontWeight = FigmaRenderHelpers.ParseFontWeight(component.FontWeight),
            TextAlignment = FigmaRenderHelpers.ParseTextAlignment(component.TextAlign),
            TextVerticalAlignment = FigmaRenderHelpers.ParseVerticalAlignment(component.VerticalAlign),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            LineHeight = component.LineHeight,
            ClipToBounds = false
        };

        return ApplyFrame(
            new Grid
            {
                ClipToBounds = false,
                Children = { textElement }
            },
            component);
    }
}
