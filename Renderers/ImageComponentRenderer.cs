using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FigmaJsonRenderer;

public sealed class ImageComponentRenderer : ComponentRenderer
{
    public ImageComponentRenderer(LayoutBindingResolver bindingResolver)
        : base(bindingResolver)
    {
    }

    public override FrameworkElement Render(FigmaComponent component)
    {
        Image image = new()
        {
            Stretch = Stretch.Uniform,
            Source = CreateImageSource(component.Src)
        };

        return ApplyFrame(image, component);
    }

    private static ImageSource? CreateImageSource(string? src)
    {
        if (string.IsNullOrWhiteSpace(src))
        {
            return null;
        }

        try
        {
            Uri uri = Uri.TryCreate(src, UriKind.Absolute, out Uri? absoluteUri)
                ? absoluteUri
                : new Uri(Path.Combine(AppContext.BaseDirectory, src.TrimStart('/', '\\')), UriKind.Absolute);

            return new BitmapImage(uri);
        }
        catch
        {
            return null;
        }
    }
}
