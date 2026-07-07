using System.Windows;

namespace FigmaJsonRenderer;

public abstract class ComponentRenderer
{
    protected ComponentRenderer(LayoutBindingResolver bindingResolver)
    {
        BindingResolver = bindingResolver;
    }

    protected LayoutBindingResolver BindingResolver { get; }

    public abstract FrameworkElement Render(FigmaComponent component);

    protected string Resolve(FigmaComponent component, string propertyName, string fallback)
    {
        if (component.Bindings is not { } bindings ||
            !bindings.TryGetValue(propertyName, out string? path))
        {
            return fallback;
        }

        return BindingResolver.Resolve(component.Slot, path) ?? fallback;
    }

    protected static T ApplyFrame<T>(T element, FigmaComponent component)
        where T : FrameworkElement
    {
        element.Width = component.Width;
        element.Height = component.Height;
        element.ToolTip = component.Name;
        return element;
    }
}
