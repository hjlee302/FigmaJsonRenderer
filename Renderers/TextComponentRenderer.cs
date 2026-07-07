using FigmaJsonRenderer.Helpers;
using System.Windows;
using System.Windows.Controls;

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

        TextBlock textBlock = new()
        {
            Text = text,
            FontSize = component.FontSize ?? 16,
            Foreground = FigmaRenderHelpers.BrushFrom(component.Color ?? "#000000"),
            FontWeight = FigmaRenderHelpers.ParseFontWeight(component.FontWeight),
            TextAlignment = FigmaRenderHelpers.ParseTextAlignment(component.TextAlign),
            VerticalAlignment = FigmaRenderHelpers.ParseVerticalAlignment(component.VerticalAlign),
            HorizontalAlignment = FigmaRenderHelpers.HorizontalAlignmentForText(component.TextAlign),
            TextWrapping = TextWrapping.NoWrap
        };

        if (component.LineHeight is > 0)
        {
            textBlock.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            textBlock.LineHeight = component.LineHeight.Value;
        }

        return ApplyFrame(
            new Grid
            {
                Children = { textBlock }
            },
            component);
    }
}
