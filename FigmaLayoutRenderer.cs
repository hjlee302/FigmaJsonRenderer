using FigmaJsonRenderer.Helpers;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigmaJsonRenderer;

public sealed class FigmaLayoutRenderer
{
    private readonly Canvas _canvas;
    private readonly ComponentRendererRegistry _rendererRegistry;

    public FigmaLayoutRenderer(Canvas canvas, LayoutBindingResolver bindingResolver)
    {
        _canvas = canvas;
        _rendererRegistry = ComponentRendererRegistry.CreateDefault(bindingResolver);
    }

    public void Render(FigmaLayout layout)
    {
        _canvas.Children.Clear();
        _canvas.Width = layout.Canvas.Width;
        _canvas.Height = layout.Canvas.Height;
        _canvas.Background = FigmaRenderHelpers.BrushFrom(layout.Canvas.Background);

        foreach (FigmaComponent component in layout.Components)
        {
            ComponentRenderer renderer = _rendererRegistry.GetRenderer(component);
            var element = renderer.Render(component);

            Canvas.SetLeft(element, component.X);
            Canvas.SetTop(element, component.Y);
            _canvas.Children.Add(element);
        }
    }
}
