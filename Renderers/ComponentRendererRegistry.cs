namespace FigmaJsonRenderer;

public sealed class ComponentRendererRegistry
{
    private readonly Dictionary<string, ComponentRenderer> _renderers;
    private readonly ComponentRenderer _ticketNumberRenderer;
    private readonly ComponentRenderer _unsupportedRenderer;

    private ComponentRendererRegistry(
        Dictionary<string, ComponentRenderer> renderers,
        ComponentRenderer ticketNumberRenderer,
        ComponentRenderer unsupportedRenderer)
    {
        _renderers = renderers;
        _ticketNumberRenderer = ticketNumberRenderer;
        _unsupportedRenderer = unsupportedRenderer;
    }

    public static ComponentRendererRegistry CreateDefault(LayoutBindingResolver bindingResolver)
    {
        ComponentRenderer textRenderer = new TextComponentRenderer(bindingResolver);
        ComponentRenderer unsupportedRenderer = new UnsupportedComponentRenderer(bindingResolver);

        return new ComponentRendererRegistry(
            new Dictionary<string, ComponentRenderer>(StringComparer.OrdinalIgnoreCase)
            {
                ["rectangle"] = new RectangleComponentRenderer(bindingResolver),
                ["text"] = textRenderer,
                ["icon"] = new IconComponentRenderer(bindingResolver),
                ["image"] = new ImageComponentRenderer(bindingResolver),
                ["horizontalLine"] = new HorizontalLineComponentRenderer(bindingResolver),
                ["blinkingText"] = new BlinkingTextComponentRenderer(bindingResolver)
            },
            new TicketNumberComponentRenderer(bindingResolver, textRenderer),
            unsupportedRenderer);
    }

    public ComponentRenderer GetRenderer(FigmaComponent component)
    {
        if (string.Equals(component.Type, "text", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(component.Role, "ticketNumber", StringComparison.OrdinalIgnoreCase))
        {
            return _ticketNumberRenderer;
        }

        return _renderers.TryGetValue(component.Type, out ComponentRenderer? renderer)
            ? renderer
            : _unsupportedRenderer;
    }
}
