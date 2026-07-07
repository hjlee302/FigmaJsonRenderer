using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigmaJsonRenderer.Helpers;

public static class FigmaRenderHelpers
{
    public static Brush BrushFrom(string value)
    {
        try
        {
            return (Brush)new BrushConverter().ConvertFromString(value)!;
        }
        catch
        {
            return Brushes.Transparent;
        }
    }

    public static FontWeight ParseFontWeight(string? value)
    {
        return string.Equals(value, "bold", StringComparison.OrdinalIgnoreCase)
            ? FontWeights.Bold
            : FontWeights.Normal;
    }

    public static TextAlignment ParseTextAlignment(string? value)
    {
        return value?.ToLowerInvariant() switch
        {
            "center" => TextAlignment.Center,
            "right" => TextAlignment.Right,
            _ => TextAlignment.Left
        };
    }

    public static HorizontalAlignment HorizontalAlignmentForText(string? value)
    {
        return string.Equals(value, "center", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(value, "right", StringComparison.OrdinalIgnoreCase)
            ? HorizontalAlignment.Stretch
            : HorizontalAlignment.Left;
    }

    public static VerticalAlignment ParseVerticalAlignment(string? value)
    {
        return value?.ToLowerInvariant() switch
        {
            "middle" => VerticalAlignment.Center,
            "bottom" => VerticalAlignment.Bottom,
            _ => VerticalAlignment.Top
        };
    }
}
