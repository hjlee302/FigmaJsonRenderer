using System.Windows;

namespace FigmaJsonRenderer;

public sealed class TicketNumberComponentRenderer : ComponentRenderer
{
    private readonly ComponentRenderer _textRenderer;

    public TicketNumberComponentRenderer(
        LayoutBindingResolver bindingResolver,
        ComponentRenderer textRenderer)
        : base(bindingResolver)
    {
        _textRenderer = textRenderer;
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        // Kmos.DisplayBoard처럼 ticketNumber는 일반 text와 분리해 둔다.
        // 실제 호출 번호 API가 연결되면 여기에서 text 값을 치환하고 깜빡임을 적용하면 된다.
        return _textRenderer.Render(component);
    }
}
